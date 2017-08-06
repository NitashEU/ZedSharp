using System;

namespace ZedSharp.Utils
{
    public class ZedException : Exception
    {
        public ZedException(int errorCode) : base("A ZedException with ErrorCode: " + errorCode + " has been thrown!")
        {
            ErrorCode = errorCode;
        }

        public int ErrorCode { get;  }
    }
}