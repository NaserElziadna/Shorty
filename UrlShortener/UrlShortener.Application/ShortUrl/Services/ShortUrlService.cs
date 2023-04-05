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

        public ShortUrlService(IUnitOfWork unitOfWork, IHashingService hashingService)
        {
            _unitOfWork = unitOfWork;
            _hashingService = hashingService;
        }

        public async Task<CreateShortUrlResponseDTO> CreateShortUrlAsync(CreateShortUrlRequestDTO request)
        {
            var url = new Domain.Entities.ShortUrl
            {
                OriginalUrl = request.LongUrl,
                ExpirationDate = request.ExpirationDate
            };

            // Generate a unique hash code for the short URL
            var hash = _hashingService.GenerateHash(url.OriginalUrl);

            var suInDB = await _unitOfWork.shortUrls.GetByShortUrlHashAsync(hash);
            // Check if the hash code already exists in the database
            while (suInDB != null)
            {
                hash = _hashingService.GenerateHash(url.OriginalUrl);
                suInDB = await _unitOfWork.shortUrls.GetByShortUrlHashAsync(hash);
            }

            // Save the short URL to the database
            url.ShortUrlHash = new Domain.Entities.ShortUrlHash
            {
                Hash = hash.GetHash(),
            };
            _unitOfWork.shortUrls.Add(url);

            await _unitOfWork.SaveChangesAsync();

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
    }
}

