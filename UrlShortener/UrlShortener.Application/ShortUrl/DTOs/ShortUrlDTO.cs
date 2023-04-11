
using UrlShortener.Domain.ValueObjects;

namespace UrlShortener.Application.ShortUrl.DTOs
{
    public class ShortUrlDTO
    {
        public int id { get; set; }
        public string OriginalUrl { get; set; }
        public ShortUrlHash ShortUrl { get; set; }
        public string ExpirationDate { get; set; }

        public long VisitsCount { get; set; }
        public System.Collections.Generic.List<Domain.Entities.StatisticLocation> Locations { get; set; }
    }
}
