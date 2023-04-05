using System;
using UrlShortener.Domain.Exceptions;

namespace UrlShortener.Domain.Entities
{
    public class ShortUrl : BaseEntity<int>
    {
        public string OriginalUrl { get; set; }
        public ShortUrlHash ShortUrlHash { get; set; }
        public string? ExpirationDate { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public ShortUrl(string originalUrl, ShortUrlHash shortUrlHash)
        {
            if (string.IsNullOrWhiteSpace(originalUrl))
            {
                throw new InvalidUrlException("Original URL cannot be empty or null");
            }

            if (shortUrlHash == null)
            {
                throw new ArgumentNullException(nameof(shortUrlHash));
            }

            OriginalUrl = originalUrl;
            ShortUrlHash = shortUrlHash;
        }

        public ShortUrl()
        {
        }
    }
}
