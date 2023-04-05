using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Interfaces;

namespace UrlShortener.Controllers
{
    public class UsersController : Controller
    {

        private UserManager<User> _userManager;
        RoleManager<IdentityRole> _roleManager;
        private IUnitOfWork _unitOfWork;
        private IHostingEnvironment _env;
        protected ILookupNormalizer _normalizer;

        public UsersController(RoleManager<IdentityRole> roleManager,
                                UserManager<User> userManager,
                                ILookupNormalizer normalizer,
                                IUnitOfWork unitOfWork, IHostingEnvironment env)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _normalizer = normalizer;
            _env = env;
            _unitOfWork = unitOfWork;
        }
        //public async Task<IActionResult> Index()
        //{
        //    List<User> users = new List<User>();

        //    if (User.IsInRole("Admin"))
        //    {
        //        users = await _userManager.Users.ToListAsync();
        //    }
        //    else if (User.IsInRole("Eshkol"))
        //    {
        //        ///return all users in which Eshkol  user id=s responsible of them
        //        ///1. get all cities by Eshkol users (city indexes)
        //        ///2. search users by cities
        //        ///3. return them

        //        User currentUser = await _userManager.GetUserAsync(User);
        //        if (currentUser.Eshkol != null)
        //        {
        //            users.AddRange(_userManager.GetUsersInRoleAsync("CityAdmin").Result.ToList());
        //            users.AddRange(_userManager.GetUsersInRoleAsync("ProjectAdmin").Result.ToList());
        //            users.AddRange(_userManager.GetUsersInRoleAsync("Rakaz").Result.ToList());
        //            users.Remove(currentUser);
        //            var temp = new List<User>();
        //            //TODO:
        //            //filter all returned users

        //            var eshkolId = currentUser.Eshkol;

        //            var cities = _unitOfWork.City1s.GetAllCity1s().Where(c => c.Eshkol == eshkolId).ToList();

        //            foreach (var item in users)
        //            {
        //                foreach (var city in cities)
        //                {
        //                    if (item.CityId == city.City_Num)
        //                    {
        //                        temp.Add(item);
        //                        //break;
        //                    }
        //                }
        //            }
        //            users = temp;
        //        }

        //    }
        //    else if (User.IsInRole("CityAdmin"))
        //    {
        //        User currentUser = await _userManager.GetUserAsync(User);
        //        if (currentUser != null && currentUser.CityId > 0)
        //        {
        //            users = await _userManager.Users.Where(user => user.CityId == currentUser.CityId).ToListAsync();
        //        }
        //    }

        //    var userRolesViewModel = new List<UserRolesViewModel>();
        //    foreach (User user in users)
        //    {
        //        var thisViewModel = new UserRolesViewModel();
        //        thisViewModel.UserId = user.Id;
        //        thisViewModel.Email = user.Email;
        //        thisViewModel.FirstName = user.FirstName;
        //        thisViewModel.LastName = user.LastName;
        //        thisViewModel.Roles = await GetUserRoles(user);
        //        thisViewModel.Eshkol = user.Eshkol;
        //        userRolesViewModel.Add(thisViewModel);
        //    }
        //    return View(userRolesViewModel);
        //}
    }
}
