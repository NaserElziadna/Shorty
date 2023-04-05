using System;

namespace UrlShortener.Domain.Exceptions
{
    public class ShortUrlNotFoundException : Exception
    {
        public ShortUrlNotFoundException() : base("Short URL not found") { }
    }
}
