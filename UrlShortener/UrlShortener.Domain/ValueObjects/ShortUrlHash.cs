using System;

namespace UrlShortener.Domain.ValueObjects
{
    public class ShortUrlHash : ValueObject<ShortUrlHash>
    {
        public int Id { get; set; }
        public string _hash { get; set; }

        public ShortUrlHash()
        {
        }

        public ShortUrlHash(string hash)
        {
            if (string.IsNullOrWhiteSpace(hash))
            {
                throw new ArgumentNullException(nameof(hash));
            }

            _hash = hash;
        }
        public string GetHash()
        {
            return _hash;
        }

        protected override bool EqualsCore(ShortUrlHash other)
        {
            return _hash == other._hash;
        }

        protected override int GetHashCodeCore()
        {
            return _hash.GetHashCode();
        }
    }
}
