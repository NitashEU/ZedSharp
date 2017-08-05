using System;
using System.Threading;
using ZedSharp.Utils;

namespace ZedSharp.RateLimit
{
    internal class FixedTokenBucket
    {
        private readonly SemaphoreSlim _sem = new SemaphoreSlim(1, 1);
        private readonly int _capacity;
        private readonly int _interval;
        private long _nextRefillTime;
        private int _tokens;

        public FixedTokenBucket(int capacity, int interval)
        {
            _capacity = capacity;
            _interval = interval;
        }

        public bool ShouldThrottle(out TimeSpan waitTime, out bool newPeriod)
        {
            return ShouldThrottle(1, false, out waitTime, out newPeriod);
        }

        public bool ShouldThrottle(int n, bool force, out TimeSpan waitTime, out bool newPeriod)
        {
            using (_sem.Run())
            {
                var currentTime = DateTime.UtcNow.ToUnixTimeMilliseconds();
                newPeriod = UpdateTokens(currentTime);
                if (_tokens < n && !force)
                {
                    var timeToIntervalEnd = _nextRefillTime - currentTime;
                    if (timeToIntervalEnd >= 0)
                    {
                        waitTime = TimeSpan.FromMilliseconds(timeToIntervalEnd);
                        return true;
                    }
                }

                _tokens -= n;

                waitTime = TimeSpan.Zero;
                return false;
            }
        }

        public bool UpdateTokens(long currentTime)
        {
            if (currentTime < _nextRefillTime)
            {
                return false;
            }
            _tokens = _capacity;
            _nextRefillTime = DateTime.UtcNow.ToUnixTimeMilliseconds() + _interval;
            return true;
        }

        public int CurrentTokenCount
        {
            get
            {
                using (_sem.Run())
                {
                    return _tokens;
                }
            }
        }

        public long CurrentIntervalStart
        {
            get
            {
                using (_sem.Run())
                {
                    return _nextRefillTime - _interval;
                }
            }
        }
    }
}
