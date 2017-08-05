using System;
using System.Threading;

namespace ZedSharp.Utils
{
    public class SemaphoreHelper : IDisposable
    {
        private readonly SemaphoreSlim _sem;

        public SemaphoreHelper(SemaphoreSlim sem)
        {
            _sem = sem;
        }

        public void Dispose()
        {
            _sem.Release();
        }
    }
}