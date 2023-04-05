﻿using System.Collections.Generic;
using System.Threading.Tasks;
using UrlShortener.Application.ShortUrl.DTOs;

namespace UrlShortener.Application.ShortUrl.Interfaces
{
    public interface IShortUrlService
    {
        Task<CreateShortUrlResponseDTO> CreateShortUrlAsync(CreateShortUrlRequestDTO request);
        Task<ShortUrlDTO> GetShortUrlAsync(string shortUrl);
        Task<List<ShortUrlDTO>> GetAllShortUrls();
        Task UpdateShortUrl(ShortUrlDTO shortUrl);
        Task DeleteShortUrlAsync(string shortUrl);
    }
}
