using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UrlShortener.Domain.Interfaces.Services;
using UrlShortener.Domain.ValueObjects;

namespace UrlShortener.Infrastructure.Services
{
    public class HashingService : IHashingService
    {
        private const string CHARS = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private const int HASH_LENGTH = 7;

        public string Hash(string originalUrl)
        {

            var random = new Random();
            var result = new string(
                Enumerable.Repeat(CHARS, HASH_LENGTH) // 7 is the desired length of the hash
                .Select(s => s[random.Next(s.Length)])
                .ToArray());

            return result;
            //// Generate a random string
            //var hash = new string(Enumerable.Repeat(CHARS, HASH_LENGTH)
            //    .Select(s => s[new Random().Next(s.Length)]).ToArray());

            //// Append a timestamp to ensure uniqueness
            //hash += DateTime.UtcNow.Ticks;

            //// Encode the hash using Base64
            //var encoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(hash));

            //// Replace any '+' or '/' characters with '-' and '_'
            //return encoded.Replace("+", "-").Replace("/", "_");
        }

        public ShortUrlHash GenerateHash(string longUrl)
        {
            return new ShortUrlHash(Hash(longUrl));
        }
    }
}
