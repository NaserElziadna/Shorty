using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.Application.User.DTOs
{
    public class GenAndAddUrlToCurrentUserRequest
    {
        public string OriginalUrl { get; set; }
    }
}
