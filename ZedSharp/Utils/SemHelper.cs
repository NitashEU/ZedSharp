using System;
using System.Threading;

namespace ZedSharp.Utils
{
    public static class SemHelper
    {
        public static IDisposable Run(this SemaphoreSlim sem)
        {
            sem.Wait();
            return new SemaphoreHelper(sem);
        }

        public static void Use(this SemaphoreSlim sem, Action action)
        {
            try
            {
                sem.Wait();
                action();
            }
            finally
            {
                sem.Release();
            }
        }
    }
}