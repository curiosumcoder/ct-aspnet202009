using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Northwind.Store.Data;
using Northwind.Store.Model;
using WA60.Models;
using X.PagedList;

namespace WA60.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NWContext _context;

        public HomeController(ILogger<HomeController> logger, NWContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Product1(int [] productId)
        {
            ViewBag.products = productId;

            return View(_context.Products.Take(10).ToList());
        }

        public IActionResult Product2(List<Product> products)
        {
            ViewBag.products = products;

            return View(_context.Products.Take(10).ToList());
        }

        public IActionResult Product3(List<Product> products)
        {
            ViewBag.products = products;

            return View(_context.Products.Take(10).ToList());
        }

        public IActionResult Product4(int? page)
        {
            var pageNumber = page ?? 1; 
            var products = _context.Products.ToPagedList(pageNumber, 5);

            return View(products);
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
