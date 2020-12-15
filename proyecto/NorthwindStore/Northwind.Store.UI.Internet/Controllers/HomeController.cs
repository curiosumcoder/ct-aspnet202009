using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Northwind.Store.UI.Internet.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Store.UI.Internet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RoleManager<IdentityRole> _rm;
        private readonly UserManager<IdentityUser> _um;

        public HomeController(ILogger<HomeController> logger, RoleManager<IdentityRole> rm, UserManager<IdentityUser> um)
        {
            _logger = logger;
            _rm = rm;
            _um = um;
        }

        public IActionResult Index()
        {
            return View();
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

        /// <summary>
        /// Crear roles y asignarle usuario
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public async Task<IActionResult> CreateRoles()
        {
            string[] roleNames = { "Admin", "Manager", "Member" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await _rm.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 1
                    roleResult = await _rm.CreateAsync(new IdentityRole(roleName));
                }
            }

            var user = await _um.FindByEmailAsync("gbermude@outlook.com");

            if (user != null)
            {
                await _um.AddToRoleAsync(user, "Admin");
            }

            return View("Index");
        }
    }
}
