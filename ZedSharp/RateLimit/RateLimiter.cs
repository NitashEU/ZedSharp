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

        public async Task WaitAllAsync(string method, bool wait = true)
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
                try
                {
                    await _waitForRateLimitsAsync(_appRateLimits, wait);
                    await _waitForRateLimitsAsync(_methodRateLimits[method], wait);
                }
                finally
                {
                    _sem.Release();
                }
            }
        }

        public async Task AdjustToHeaderAsync(string method, DateTime requestDate, DateTime responseDate, Dictionary<string, string> headers, bool wait = true)
        {
            using (_headerSem.Run())
            {
                var appRateLimitsAvailable = false;
                var methodRateLimitsAvailable = false;
                try
                {
                    if (headers == default(Dictionary<string, string>))
                    {
                        return;
                    }

                    appRateLimitsAvailable = _appRateLimits.Count > 0;
                    methodRateLimitsAvailable = _methodRateLimits[method].Count > 0;
                    
                    if (headers.ContainsKey(HeaderNames.AppRateLimit))
                    {
                        var appRateLimit = headers[HeaderNames.AppRateLimit];
                        var appRateLimitCount = headers[HeaderNames.AppRateLimitCount];
                        if (!appRateLimitsAvailable)
                        {
                            _appRateLimits.AddRange(_getRateLimits(appRateLimit, wait));
                        }
                        await _adjustRateLimitsAsync(appRateLimitCount, _appRateLimits, wait);
                    }
                    if (headers.ContainsKey(HeaderNames.MethodRateLimit))
                    {
                        var methodRateLimit = headers[HeaderNames.MethodRateLimit];
                        var methodRateLimitCount = headers[HeaderNames.MethodRateLimitCount];
                        if (!methodRateLimitsAvailable)
                        {
                            _methodRateLimits[method].AddRange(_getRateLimits(methodRateLimit, wait));
                        }
                        await _adjustRateLimitsAsync(methodRateLimitCount, _methodRateLimits[method], wait);
                    }

                    if (headers.ContainsKey(HeaderNames.RetryAfter))
                    {
                        if (!wait)
                        {
                            throw new ZedException(100);
                        }
                        await _waitForRetryAfterAsync(int.Parse(headers[HeaderNames.RetryAfter]), responseDate);
                    }
                }
                finally
                {
                    if (_sem.CurrentCount == 0 && (!appRateLimitsAvailable || !methodRateLimitsAvailable || _isNewPeriod))
                    {
                        _isNewPeriod = false;
                        _sem.Release();
                    }
                }

            }
        }

        private async Task _waitForRateLimitsAsync(List<RateLimit> rateLimits, bool wait)
        {
            var isNewPeriod = false;
            foreach (var rl in rateLimits)
            {
                var sub = await rl.WaitAsync(1, false, wait);
                if (!isNewPeriod && sub)
                {
                    isNewPeriod = true;
                }
            }
            _isNewPeriod = isNewPeriod && !_isNewPeriod;
        }

        private async Task _waitForRetryAfterAsync(int retryAfter, DateTime responseDate)
        {
            var now = DateTime.UtcNow.ToUnixTimeMilliseconds();
            var waitTime = retryAfter - (now - responseDate.ToUnixTimeMilliseconds()) / 1000;
            if (waitTime > 0)
            {
                await Task.Delay((int)waitTime * 1050);
            }
        }

        private async Task _adjustRateLimitsAsync(string newValues, List<RateLimit> rateLimits, bool wait)
        {
            var dic = _getRateLimitsDic(newValues);
            foreach (var rl in rateLimits)
            {
                if (dic[rl.Seconds] > rl.Max - rl.CallsLeft && rl.CallsLeft > 0 && _isNewPeriod)
                {
                    await rl.WaitAsync(dic[rl.Seconds] - (rl.Max - rl.CallsLeft), true, wait);
                }
            }
        }

        private List<RateLimit> _getRateLimits(string rateLimit, bool wait)
        {
            return _getRateLimitsDic(rateLimit).Select(kvp =>
            {
                var rl = new RateLimit((int) (kvp.Value * 0.9), kvp.Key);
                rl.WaitAsync(1, false, wait).Wait();
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
