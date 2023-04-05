using System;

namespace UrlShortener.Domain.Exceptions
{
    public class InvalidUrlException : Exception
    {
        public InvalidUrlException(string message) : base(message) { }
    }
}
