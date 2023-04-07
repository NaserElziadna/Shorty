using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetCurrentUserAsync();
    }
}
