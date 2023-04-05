namespace UrlShortener.Application.ShortUrl.DTOs
{
    public class CreateShortUrlRequestDTO
    {
        public string LongUrl { get; set; }
        public string ExpirationDate { get; set; }
    }
}
