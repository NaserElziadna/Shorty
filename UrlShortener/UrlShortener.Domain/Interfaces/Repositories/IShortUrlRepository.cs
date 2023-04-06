
using System.Threading.Tasks;
using UrlShortener.Domain.Entities;
using UrlShortener.Domain.ValueObjects;

namespace UrlShortener.Domain.Interfaces.Repositories
{
    public interface IShortUrlRepository : IRepository<ShortUrl>
    {
        Task<ShortUrl> GetByShortUrlHashAsync(ValueObjects.ShortUrlHash shortUrlHash);
        Task<ShortUrl> GetByShortUrlByIdAsync(int id);
        void Update(ShortUrl url);
        void AddShortUrl(ShortUrl url);
    }
}
