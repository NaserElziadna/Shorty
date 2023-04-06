using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Infrastructure.Data.Contexts
{
    public class UrlShortenerDbContext : IdentityDbContext<User>
    {
        private IConfiguration _config;

        public UrlShortenerDbContext()
        {
        }

        public UrlShortenerDbContext(IConfiguration config, DbContextOptions<UrlShortenerDbContext> options)
            : base(options)
        {
            _config = config;
        }
        public virtual DbSet<ShortUrl> ShortUrls { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder);
                Debug.WriteLine("Config" + _config["ConnectionStrings:ConnectionString"]);
                optionsBuilder.UseSqlServer(_config["ConnectionStrings:ConnectionString"], b=>b.MigrationsAssembly("UrlShortener.Infrastructure"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
