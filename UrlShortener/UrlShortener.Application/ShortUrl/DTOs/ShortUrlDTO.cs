
using UrlShortener.Domain.ValueObjects;

namespace UrlShortener.Application.ShortUrl.DTOs
{
    public class ShortUrlDTO
    {
        public string OriginalUrl { get; set; }
        public ShortUrlHash ShortUrl { get; set; }
        public string ExpirationDate { get; set; }
    }
}
