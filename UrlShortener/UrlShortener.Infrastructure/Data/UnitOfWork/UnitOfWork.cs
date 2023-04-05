using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;
using UrlShortener.Domain.Interfaces;
using UrlShortener.Domain.Interfaces.Repositories;
using UrlShortener.Infrastructure.Data.Contexts;
using UrlShortener.Infrastructure.Data.Repositories;

namespace UrlShortener.Infrastructure.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UrlShortenerDbContext _context;

        public UnitOfWork(UrlShortenerDbContext context, IMemoryCache memoryCache)
        {
            _context = context;

            shortUrls = new ShortUrlRepository(_context, memoryCache);
        }
        public IShortUrlRepository shortUrls { get; set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                int result = await _context.SaveChangesAsync();
                return (result > 0);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
