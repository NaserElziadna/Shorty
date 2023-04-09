using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Net.Http;
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
        protected string GetIPAddress()
        {
            var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
            return remoteIpAddress.MapToIPv4().ToString();
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
            try
            {
                var url = await _shortUrlService.GetShortUrlAsync(code);
                if (url == null)
                {
                    return RedirectToAction("CouldNotRedirect", new
                    {
                        message = $"No Url Found Under this Hash - {code}"
                    });
                }

                //validate experation date
                var expiryDate = DateTime.Now;
                DateTime.TryParse(url.ExpirationDate, out expiryDate);

                if (expiryDate < DateTime.Now)
                {
                    return RedirectToAction("CouldNotRedirect", new
                    {
                        message = $"This Has Been Expired - {code}"
                    });
                }


                UriBuilder uriBuilder = new UriBuilder(url.OriginalUrl);
                // Test our URL
                try
                {
                    // Use HttpClient to make a request to the URL
                    using (var client = new HttpClient())
                    {
                        // Add user agent header to the request
                        client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
                        var request = new HttpRequestMessage(HttpMethod.Get, uriBuilder.Uri);
                        request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
                        //request.Headers.Add("Accept", "application/json");
                        //request.Headers.Add("Authorization", "Bearer mytoken");
                        request.Headers.Add("Referer", _configration["BASE_URL"]);

                        var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

                        if (!response.IsSuccessStatusCode)
                        {
                            // Redirect to error page if there was an error accessing the URL
                            return RedirectToAction("CouldNotRedirect", new
                            {
                                message = $"Url Is Not Valid Url {code} || --> REASON :: {response.ToString()}"
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Redirect to error page if an exception occurred
                    return RedirectToAction("CouldNotRedirect", new
                    {
                        message = $"Somthing Happend when launched Test Url To check validation of the original url - {ex.Message}"
                    });
                }

                var iPAddress = GetIPAddress();
                var location = await new HttpClient().GetStringAsync(_configration["IP_API_LOOKUP"].Replace("##iPAddressReplace##", iPAddress));

                // If we made it this far, the URL is valid and we can redirect
                await _shortUrlService.addVisitToShortUrlById(url.id);

                var locationJsonObj = JObject.Parse(location);
                if (locationJsonObj.ContainsKey("status"))
                {
                    if (locationJsonObj.GetValue("status").ToString() == "success")
                    {
                        //add the location based on ip , (add only if valid response)
                        await _shortUrlService.addLocationToShortUrlById(url.id, location);
                    }
                }
                return Redirect(uriBuilder.Uri.AbsoluteUri);
            }
            catch (Exception ex)
            {

                return RedirectToAction("CouldNotRedirect", new
                {
                    message = $"Somthing Happend when launched Test Url To check validation of the original url - {ex.Message}"
                });
            }

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
                OriginalUrl = url.url,
                expiryDate = url.expiryDate
            });
            return Ok(result);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult CouldNotRedirect(string message = "Could Not Redirect")
        {
            ViewBag.message = message;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
