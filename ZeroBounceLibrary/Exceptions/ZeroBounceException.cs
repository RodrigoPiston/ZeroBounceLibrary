using System;

namespace ZeroBounceLibrary.Exceptions
{
    public class ZeroBounceException : Exception
    {
        public ZeroBounceException(string message) : base(message) { }
        public ZeroBounceException(string message, Exception innerException) : base(message, innerException) { }
    }
}
