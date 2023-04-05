using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UrlShortener.Domain;
using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Interfaces.Repositories;
using UrlShortener.Infrastructure.Data.Contexts;

namespace UrlShortener.Infrastructure.Data.Repositories
{
    public class ShortUrlRepository : Repository<ShortUrl>, IShortUrlRepository
    {
        public ShortUrlRepository(UrlShortenerDbContext portalContext,
          IMemoryCache memoryCache) : base(portalContext, memoryCache)
        {
        }

        #region Url Shortener
        public void AddShortUrl(ShortUrl entity)
        {
            _dbContext.Add(entity);
        }

        public async Task AddAsync(ShortUrl entity)
        {
            await _dbContext.ShortUrls.AddAsync(entity);
        }

        public void AddRange(IEnumerable<ShortUrl> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ShortUrl> Find(Expression<Func<ShortUrl, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ShortUrl> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ShortUrl>> GetAllAsync()
        {
            return await _dbContext.ShortUrls.ToListAsync();
        }

        public async Task<ShortUrl> GetByIdAsync(int id)
        {
            return await _dbContext.ShortUrls.FindAsync(id);
        }

        public async Task<ShortUrl> GetByShortUrlHashAsync(Domain.ValueObjects.ShortUrlHash shortUrlHash)
        {
            return await _dbContext.ShortUrls.Include(sh=>sh.ShortUrlHash).FirstOrDefaultAsync(a => a.ShortUrlHash.Hash == shortUrlHash.GetHash());
        }

        public void Remove(ShortUrl entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<ShortUrl> entities)
        {
            throw new NotImplementedException();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public ShortUrl SingleOrDefault(Expression<Func<ShortUrl, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Update(ShortUrl entity)
        {
            _dbContext.ShortUrls.Update(entity);
        }

        #endregion

        public UrlShortenerDbContext _dbContext
        {
            get { return Context as UrlShortenerDbContext; }
        }

    }
}

