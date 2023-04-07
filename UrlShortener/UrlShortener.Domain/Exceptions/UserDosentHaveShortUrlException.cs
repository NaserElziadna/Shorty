using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.Domain.Exceptions
{
    public class UserDosentHaveShortUrlException : Exception
    {
        public UserDosentHaveShortUrlException() : base("User Dosent Have Any Links on him") { }
    }
}
