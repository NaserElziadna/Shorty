using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using UrlShortener.Application.ShortUrl.Interfaces;
using UrlShortener.Application.Utils;
using UrlShortener.Models;

namespace UrlShortener.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IShortUrlService _shortUrlService;

        public HomeController(ILogger<HomeController> logger, IShortUrlService shortUrlService)
        {
            _logger = logger;
            _shortUrlService = shortUrlService;
        }

        public async Task<IActionResult> Index()
        {
            //var result = await _shortUrlService.CreateShortUrlAsync(new Application.ShortUrl.DTOs.CreateShortUrlRequestDTO
            //{
            //    LongUrl = "www.google.com",
            //    ExpirationDate = DateTime.Now.ToString("yyyy-MM-dd"),
            //});

            return View();
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> RedirectTo(string code)
        {
            var url = await _shortUrlService.GetShortUrlAsync(code);

            //var isValidUrl = UrlValidator.IsValidUrl("http://dasdasdasd");
            if (url == null)
            {
                return NotFound();
            }

            return Redirect(url.OriginalUrl);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
