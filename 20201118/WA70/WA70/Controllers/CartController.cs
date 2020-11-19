using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.Store.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WA70.Controllers
{
    public class CartController : Controller
    {
        private readonly NWContext _context;
        private readonly SessionSettings _ss;

        public CartController(NWContext context, SessionSettings ss)
        {
            _context = context;
            _ss = ss;
        }
        public IActionResult Index()
        {
            ViewBag.welcome = HttpContext.Session.GetString("Welcome");

            return View(_ss.Cart);
        }

        public ActionResult Add(int? id)
        {
            if (id.HasValue)
            {
                var cart = _ss.Cart;

                cart.Items.Add(_context.Products.Find(id));

                _ss.Cart = cart;
            }

            return RedirectToAction("Index");
        }
    }
}
