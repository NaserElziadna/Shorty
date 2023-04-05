using System;

namespace UrlShortener.Domain.ValueObjects
{
    public class Url : ValueObject<Url>
    {
        private readonly string _url;
        public string LongUrl { get; set; }
        public string ExpirationDate { get; set; }
        public ShortUrlHash ShortUrl { get; set; }

        public Url(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            _url = url;
        }

        public Url(string url, string longUrl, string expirationDate) : this(url)
        {
            LongUrl = longUrl;
            ExpirationDate = expirationDate;
        }

        public Url()
        {
        }

        protected override bool EqualsCore(Url other)
        {
            return _url == other._url;
        }

        protected override int GetHashCodeCore()
        {
            return _url.GetHashCode();
        }
    }
}
