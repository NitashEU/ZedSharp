using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ZedSharp.Constants;
using ZedSharp.Utils;

namespace ZedSharp.RateLimit
{
    internal class RateLimiter
    {
        private readonly SemaphoreSlim _sem = new SemaphoreSlim(1, 1);
        private readonly SemaphoreSlim _headerSem = new SemaphoreSlim(1, 1);
        private readonly List<RateLimit> _appRateLimits = new List<RateLimit>();
        private readonly Dictionary<string, List<RateLimit>> _methodRateLimits = new Dictionary<string, List<RateLimit>>();
        private bool _isNewPeriod = true;

        public async Task WaitAll(string method)
        {
            _prepareMethodRateLimits(method);

            _sem.Wait();

            var appRateLimitsAvailable = _appRateLimits.Count > 0;
            var methodRateLimitsAvailable = _methodRateLimits[method].Count > 0;

            if (!appRateLimitsAvailable || !methodRateLimitsAvailable || _isNewPeriod)
            {
                return;
            }

            using (_headerSem.Run())
            {
                await _waitForRateLimits(_appRateLimits);
                await _waitForRateLimits(_methodRateLimits[method]);
                _sem.Release();
            }
        }

        public async Task AdjustToHeader(string method, DateTime requestDate, DateTime responseDate, Dictionary<string, string> headers)
        {
            using (_headerSem.Run())
            {
                if (headers == default(Dictionary<string, string>))
                {
                    return;
                }

                var appRateLimit = headers[HeaderNames.AppRateLimit];
                var methodRateLimit = headers[HeaderNames.MethodRateLimit];
                var appRateLimitCount = headers[HeaderNames.AppRateLimitCount];
                var methodRateLimitCount = headers[HeaderNames.MethodRateLimitCount];

                var appRateLimitsAvailable = _appRateLimits.Count > 0;
                var methodRateLimitsAvailable = _methodRateLimits[method].Count > 0;

                if (!appRateLimitsAvailable)
                {
                    _appRateLimits.AddRange(_getRateLimits(appRateLimit));
                }

                if (!methodRateLimitsAvailable)
                {
                    _methodRateLimits[method].AddRange(_getRateLimits(methodRateLimit));
                }

                await _adjustRateLimits(appRateLimitCount, _appRateLimits);
                await _adjustRateLimits(methodRateLimitCount, _methodRateLimits[method]);

                if (headers.ContainsKey(HeaderNames.RetryAfter))
                {
                    await _waitForRetryAfter(int.Parse(headers[HeaderNames.RetryAfter]), responseDate);
                }

                if (_sem.CurrentCount == 0 && (!appRateLimitsAvailable || !methodRateLimitsAvailable || _isNewPeriod))
                {
                    _isNewPeriod = false;
                    _sem.Release();
                }
            }
        }

        private async Task _waitForRateLimits(List<RateLimit> rateLimits)
        {
            var isNewPeriod = false;
            foreach (var rl in rateLimits)
            {
                var sub = await rl.Wait();
                if (!isNewPeriod && sub)
                {
                    isNewPeriod = true;
                }
            }
            _isNewPeriod = isNewPeriod && !_isNewPeriod;
        }

        private async Task _waitForRetryAfter(int retryAfter, DateTime responseDate)
        {
            var now = DateTime.UtcNow.ToUnixTimeMilliseconds();
            var waitTime = retryAfter - (now - responseDate.ToUnixTimeMilliseconds()) / 1000;
            if (waitTime > 0)
            {
                await Task.Delay((int)waitTime * 1050);
            }
        }

        private async Task _adjustRateLimits(string newValues, List<RateLimit> rateLimits)
        {
            var dic = _getRateLimitsDic(newValues);
            foreach (var rl in rateLimits)
            {
                if (dic[rl.Seconds] > rl.Max - rl.CallsLeft && rl.CallsLeft > 0 && _isNewPeriod)
                {
                    await rl.Wait(dic[rl.Seconds] - (rl.Max - rl.CallsLeft), true);
                }
            }
        }

        private List<RateLimit> _getRateLimits(string rateLimit)
        {
            return _getRateLimitsDic(rateLimit).Select(kvp =>
            {
                var rl = new RateLimit((int) (kvp.Value * 0.9), kvp.Key);
                rl.Wait().Wait();
                return rl;
            }).ToList();
        }

        private Dictionary<int, int> _getRateLimitsDic(string rateLimit)
        {
            return rateLimit.Split(',').ToDictionary(s => int.Parse(s.Split(':')[1]), s => int.Parse(s.Split(':')[0]));
        }

        private void _prepareMethodRateLimits(string method)
        {
            if (!_methodRateLimits.ContainsKey(method))
            {
                _methodRateLimits.Add(method, new List<RateLimit>());
            }
        }
    }
}
