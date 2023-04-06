using System;
using System.Threading.Tasks;
using UrlShortener.Domain.Interfaces.Repositories;

namespace UrlShortener.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IShortUrlRepository shortUrls { get; }
        IUserRepository users { get; }


        int Complete();

        Task<bool> SaveChangesAsync();
    }
}
