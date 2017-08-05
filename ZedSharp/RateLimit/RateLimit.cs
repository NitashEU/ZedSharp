using System;
using System.Threading.Tasks;

namespace ZedSharp.RateLimit
{
    internal class RateLimit
    {
        public int Seconds { get; }
        public int Max { get; }
        public int CallsLeft => _tokenBucket.CurrentTokenCount;
        private readonly FixedTokenBucket _tokenBucket;

        public RateLimit(int max, int seconds)
        {
            Seconds = seconds;
            Max = max;
            var interval = seconds * 1025;
            _tokenBucket = new FixedTokenBucket(max, interval);
        }

        public async Task<bool> Wait(int n = 1, bool force = false)
        {
            TimeSpan waitTime;
            bool newPeriod;
            if (_tokenBucket.ShouldThrottle(n, force, out waitTime, out newPeriod))
            {
                await Task.Delay(waitTime);
                return await Wait(n, force);
            }
            if (newPeriod) Console.WriteLine("NEW PERIOD: " + Seconds);
            return newPeriod;
        }
    }
}
