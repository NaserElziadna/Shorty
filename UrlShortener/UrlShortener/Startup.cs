using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UrlShortener.Application.ShortUrl.Interfaces;
using UrlShortener.Application.ShortUrl.Services;
using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Interfaces;
using UrlShortener.Domain.Interfaces.Services;
using UrlShortener.Infrastructure.Data.Contexts;
using UrlShortener.Infrastructure.Data.UnitOfWork;
using UrlShortener.Infrastructure.Services;

namespace UrlShortener
{
    public class Startup
    {
        public Microsoft.Extensions.Configuration.IConfiguration _config { get; }

        private IWebHostEnvironment _env;
        public Startup(Microsoft.Extensions.Configuration.IConfiguration configuration, IWebHostEnvironment env)
        {
            _env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)
                .AddJsonFile("config.json");

            _config = builder.Build();

            configuration = _config;

        }

        public AutoMapper.IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddLogging();
            services.AddSingleton(_config);
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IHashingService, HashingService>();
            services.AddTransient<IShortUrlService, ShortUrlService>();
            //services.ConfigureApplicationCookie(opts =>
            //{
            //    opts.AccessDeniedPath = "/Helpers/AccessDenied";
            //});

            services.AddIdentity<User, IdentityRole>(/*option => option.SignIn.RequireConfirmedAccount = true*/)
                .AddEntityFrameworkStores<UrlShortenerDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();
            services.AddDbContext<UrlShortenerDbContext>();

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
