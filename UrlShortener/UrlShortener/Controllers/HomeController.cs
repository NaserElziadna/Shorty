using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using UrlShortener.Application.ShortUrl.Interfaces;
using UrlShortener.Application.User.Interfaces;
using UrlShortener.Models;

namespace UrlShortener.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IShortUrlService _shortUrlService;
        private readonly IUserService _userService;
        private readonly IConfiguration _configration;
        private UserManager<Domain.Entities.User> _userManager;



        public HomeController(ILogger<HomeController> logger, IShortUrlService shortUrlService, IUserService userService, UserManager<Domain.Entities.User> userManager, IConfiguration configration)
        {
            _logger = logger;
            _shortUrlService = shortUrlService;
            _userService = userService;
            _userManager = userManager;
            _configration = configration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet()]
        [Route("r/{code}")]
        [AllowAnonymous]
        public async Task<IActionResult> RedirectTo(string code)
        {
            var url = await _shortUrlService.GetShortUrlAsync(code);
            if (url == null)
            {
                return RedirectToAction("CouldNotRedirect");
            }

            UriBuilder uriBuilder = new UriBuilder(url.OriginalUrl);
            // Test our URL
            try
            {
                // Use HttpClient to make a request to the URL
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(uriBuilder.Uri);

                    if (!response.IsSuccessStatusCode)
                    {
                        // Redirect to error page if there was an error accessing the URL
                        return RedirectToAction("CouldNotRedirect");
                    }
                }
            }
            catch (Exception ex)
            {
                // Redirect to error page if an exception occurred
                return RedirectToAction("CouldNotRedirect");
            }

            // If we made it this far, the URL is valid and we can redirect
            await _shortUrlService.addVisitToShortUrlById(url.id);

            return Redirect(uriBuilder.Uri.AbsoluteUri);
        }

        public async Task<IActionResult> Statistics()
        {
            var result = (dynamic)null;

            try
            {
                result = await _userService.GetShortUrlsForCurrentUserAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View(result);
        }

        [HttpGet()]
        [Route("getUserStatistic")]
        public async Task<IActionResult> GetUserStatisticAPI()
        {
            var result = (dynamic)null;

            try
            {
                result = await _userService.GetShortUrlsForCurrentUserAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Json(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost()]
        [Route("shortenUrl")]
        public async Task<IActionResult> ShortenUrlAsync([FromBody] UrlType url)
        {

            var result = await _userService.GenAndAddShortUrlToCurrentUserAsync(new Application.User.DTOs.GenAndAddUrlToCurrentUserRequest
            {
                OriginalUrl = url.url
            });
            return Ok(result);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult CouldNotRedirect()
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
