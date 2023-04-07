using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortener.Application.ShortUrl.DTOs;

namespace UrlShortener.Application.User.DTOs
{
    public class UserDTO
    {
        public List<ShortUrlDTO> links { get; set; }
    }
}
