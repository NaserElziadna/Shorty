using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Exceptions;
using UrlShortener.Domain.Interfaces.Repositories;
using UrlShortener.Infrastructure.Data.Contexts;

namespace UrlShortener.Infrastructure.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private UserManager<Domain.Entities.User> _userManager;

        public UserRepository(UrlShortenerDbContext portalContext,
         IMemoryCache memoryCache, UserManager<Domain.Entities.User> userManager, IHttpContextAccessor httpContextAccessor) : base(portalContext, memoryCache)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        #region User
        public async Task<User> GetCurrentUserAsync()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            var lazyLoadedUser = _dbContext.Users
                .Include(s => s.ShortUrls)
                    .ThenInclude(a => a.ShortUrlHash)
                .Include(u => u.ShortUrls)
                    .ThenInclude(s => s.LinkStatistics)
                .FirstOrDefault(a => a.Id == user.Id);//for lazy loading prop like "ShortUrls"

            if (lazyLoadedUser == null)
            {
                throw new UserNotFoundException();
            }

            return lazyLoadedUser;
        }
        #endregion

        public UrlShortenerDbContext _dbContext
        {
            get { return Context as UrlShortenerDbContext; }
        }


    }
}
