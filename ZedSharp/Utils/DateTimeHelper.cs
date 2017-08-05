using System;

namespace ZedSharp.Utils
{
    public static class DateTimeHelper
    {
        public static long ToUnixTimeMilliseconds(this DateTime dateTime)
        {
            return ((DateTimeOffset) dateTime).ToUnixTimeMilliseconds();
        }
    }
}
