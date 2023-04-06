using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrlShortener.Application.ShortUrl.DTOs;
using UrlShortener.Application.ShortUrl.Interfaces;
using UrlShortener.Domain.Interfaces;
using UrlShortener.Domain.Interfaces.Services;
using UrlShortener.Domain.ValueObjects;

namespace UrlShortener.Application.ShortUrl.Services
{
    public class ShortUrlService : IShortUrlService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHashingService _hashingService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private UserManager<Domain.Entities.User> _userManager;

        public ShortUrlService(UserManager<Domain.Entities.User> userManager, IUnitOfWork unitOfWork, IHashingService hashingService, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _hashingService = hashingService;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CreateShortUrlResponseDTO> CreateShortUrlAsync(CreateShortUrlRequestDTO request)
        {
            // Generate a unique hash code for the short URL
            var hash = _hashingService.GenerateHash(request.LongUrl);

            var suInDB = await _unitOfWork.shortUrls.GetByShortUrlHashAsync(hash);
            // Check if the hash code already exists in the database
            while (suInDB != null)
            {
                hash = _hashingService.GenerateHash(request.LongUrl);
                suInDB = await _unitOfWork.shortUrls.GetByShortUrlHashAsync(hash);
            }

            return new CreateShortUrlResponseDTO
            {
                ShortUrl = hash.GetHash()
            };
        }

        public async Task<ShortUrlDTO> GetShortUrlAsync(string shortUrl)
        {
            var url = await _unitOfWork.shortUrls.GetByShortUrlHashAsync(new ShortUrlHash(shortUrl));

            if (url == null)
            {
                return null;
            }

            return new ShortUrlDTO
            {
                id = url.Id,
                OriginalUrl = url.OriginalUrl,
                ShortUrl = new ShortUrlHash(url.ShortUrlHash.Hash),
                ExpirationDate = url.ExpirationDate
            };
        }

        public async Task<List<ShortUrlDTO>> GetAllShortUrls()
        {
            var urls = _unitOfWork.shortUrls.GetAll();

            var shortUrls = new List<ShortUrlDTO>();

            foreach (var url in urls)
            {
                shortUrls.Add(new ShortUrlDTO
                {
                    OriginalUrl = url.OriginalUrl,
                    ShortUrl = new ShortUrlHash(url.ShortUrlHash.Hash),
                    ExpirationDate = url.ExpirationDate
                });
            }

            return shortUrls;
        }

        public async Task UpdateShortUrl(ShortUrlDTO shortUrl)
        {
            var url = await _unitOfWork.shortUrls.GetByShortUrlHashAsync(new ShortUrlHash(shortUrl.ShortUrl.GetHash()));

            if (url == null)
            {
                return;
            }

            url.OriginalUrl = shortUrl.OriginalUrl;
            url.ExpirationDate = shortUrl.ExpirationDate;

            _unitOfWork.shortUrls.Update(url);
        }

        public async Task DeleteShortUrlAsync(string shortUrl)
        {
            var url = await _unitOfWork.shortUrls.GetByShortUrlHashAsync(new ShortUrlHash(shortUrl));

            if (url == null)
            {
                return;
            }

            _unitOfWork.shortUrls.Remove(url);
        }

        public async Task addVisitToShortUrlById(int id)
        {
            var shortUrl = await _unitOfWork.shortUrls.GetByShortUrlByIdAsync(id);
            if (shortUrl != null)
            {
                if (shortUrl.LinkStatistics == null)
                {
                    shortUrl.LinkStatistics = new Domain.Entities.LinkStatistics
                    {
                        VisitsCount = 0,
                    };
                }
                shortUrl.LinkStatistics.VisitsCount++;

                _unitOfWork.shortUrls.Update(shortUrl);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}

