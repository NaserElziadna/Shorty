using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace UrlShortener.Domain.Entities
{
    // User.cs
    public class User : IdentityUser
    {
        public List<ShortUrl> ShortUrls { get; set; }
        public User()
        {
        }

    }
}
