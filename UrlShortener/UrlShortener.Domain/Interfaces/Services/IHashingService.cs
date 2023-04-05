using UrlShortener.Domain.ValueObjects;

namespace UrlShortener.Domain.Interfaces.Services
{
    public interface IHashingService
    {
        string Hash(string text);
        ShortUrlHash GenerateHash(string longUrl);
    }
}
