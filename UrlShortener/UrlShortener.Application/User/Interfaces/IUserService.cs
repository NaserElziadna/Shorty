using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortener.Application.User.DTOs;

namespace UrlShortener.Application.User.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> GetShortUrlsForUserAsync(string userId);
        Task<UserDTO> GetShortUrlsForCurrentUserAsync();
        Task<GenAndAddUrlToCurrentUserResponse> GenAndAddShortUrlToCurrentUserAsync(GenAndAddUrlToCurrentUserRequest request);
        Task AddShortUrlToUserAsync(string userId, UserDTO userDTO);
        Task RemoveShortUrlFromUserAsync(string userId, string shortUrlId);
    }
}
