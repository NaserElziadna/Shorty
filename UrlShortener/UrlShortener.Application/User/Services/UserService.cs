using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortener.Application.ShortUrl.Interfaces;
using UrlShortener.Application.User.DTOs;
using UrlShortener.Application.User.Interfaces;
using UrlShortener.Domain.Exceptions;
using UrlShortener.Domain.Interfaces;

namespace UrlShortener.Application.User.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private UserManager<Domain.Entities.User> _userManager;
        private readonly IShortUrlService _shortUrlService;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(UserManager<Domain.Entities.User> userManager, IHttpContextAccessor httpContextAccessor, IShortUrlService shortUrlService, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _shortUrlService = shortUrlService;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDTO> GetShortUrlsForCurrentUserAsync()
        {
            try
            {
                var fullUser = await _unitOfWork.users.GetCurrentUserAsync();

                if (fullUser.ShortUrls == null)
                {
                    //do somthing here
                    throw new UserDosentHaveShortUrlException();
                }

                var result = new UserDTO
                {
                    links = new List<ShortUrl.DTOs.ShortUrlDTO>()
                };

                foreach (var item in fullUser.ShortUrls)
                {
                    result.links.Add(new ShortUrl.DTOs.ShortUrlDTO
                    {
                        OriginalUrl = item.OriginalUrl,
                        ShortUrl = new Domain.ValueObjects.ShortUrlHash(item.ShortUrlHash.Hash),
                        ExpirationDate = item.ExpirationDate,
                        VisitsCount = item.LinkStatistics != null ? item.LinkStatistics.VisitsCount : 0
                    });
                }

                return result;
            }
            catch (UserNotFoundException ex)
            {
                //do somthing here
                throw;// rethrow catched exception
            }
        }

        public async Task<GenAndAddUrlToCurrentUserResponse> GenAndAddShortUrlToCurrentUserAsync(GenAndAddUrlToCurrentUserRequest request)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            var tmpUser = user;
            if (tmpUser.ShortUrls == null)//in case that the user didnt generate any short-links
            {
                tmpUser.ShortUrls = new List<Domain.Entities.ShortUrl>();
            }

            var shortUrl = await _shortUrlService.CreateShortUrlAsync(new ShortUrl.DTOs.CreateShortUrlRequestDTO
            {
                LongUrl = request.OriginalUrl,
                ExpirationDate = DateTime.Now.ToString("d")
            });

            tmpUser.ShortUrls.Add(new Domain.Entities.ShortUrl
            {
                OriginalUrl = request.OriginalUrl,
                ExpirationDate = DateTime.Now.ToString("d"),
                ShortUrlHash = new Domain.Entities.ShortUrlHash
                {
                    Hash = shortUrl.ShortUrl,
                },
                LinkStatistics = new Domain.Entities.LinkStatistics
                {
                    VisitsCount = 0
                }
            });

            await _userManager.UpdateAsync(tmpUser);

            return new GenAndAddUrlToCurrentUserResponse
            {
                Hash = shortUrl.ShortUrl
            };
        }
        public Task<UserDTO> GetShortUrlsForUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task AddShortUrlToUserAsync(string userId, UserDTO userDTO)
        {
            throw new NotImplementedException();
        }

        public Task RemoveShortUrlFromUserAsync(string userId, string shortUrlId)
        {
            throw new NotImplementedException();
        }


    }
}
