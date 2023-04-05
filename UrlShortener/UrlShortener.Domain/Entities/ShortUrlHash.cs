namespace UrlShortener.Domain.Entities
{
    public class ShortUrlHash : BaseEntity<int>
    {
        public string Hash { get; set; }
    }
}
