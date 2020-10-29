using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WA4.Models;
using Northwind.Store.Data;
using Microsoft.EntityFrameworkCore;
using WA4.ViewModels;
using WA4.Extensions;

namespace WA4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        readonly NWContext db;

        public HomeController(ILogger<HomeController> logger, NWContext db)
        {
            _logger = logger;
            this.db = db;
        }

        public IActionResult Index(string filter = "")
        {
            var q1 = from p in db.Products.Include(p => p.Category).ToList()
                     where string.IsNullOrEmpty(filter) ||
                     p.ProductName.Contains(filter, StringComparison.InvariantCultureIgnoreCase)                     
                     group p by p.Category?.CategoryName ?? "Sin Categoría"  into CategoryProducts
                     select new CategoryProductsViewModel()
                     {
                         CategoryName = CategoryProducts.Key,
                         Items = CategoryProducts.ToList()
                     };

            ViewBag.filter = filter;

            return View(q1.ToList());
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

        public IActionResult IndexPartial(int? id)
        {
            var isAjax = Request.IsAjaxRequest();

            if (id != null)
            {
                return PartialView("ProductPartial", db.Products.Where(p => p.ProductId == id).SingleOrDefault());
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult IndexViewComponent(int? id)
        {
            var isAjax = Request.IsAjaxRequest();

            if (id != null)
            {
                return ViewComponent("Product", new { id });
            }
            else
            {
                return NotFound();
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            db.Dispose();
        }
    }
}
