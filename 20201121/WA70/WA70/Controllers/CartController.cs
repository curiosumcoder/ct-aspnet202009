using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.Store.Data;
using Northwind.Store.Model;
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
        private readonly RequestSettings _rs;

        public CartController(NWContext context, SessionSettings ss)
        {
            _context = context;
            _ss = ss;

            _rs = new RequestSettings(this);
        }
        public IActionResult Index()
        {
            ViewBag.welcome = HttpContext.Session.GetString("Welcome");

            #region TempData
            var productId = TempData[nameof(Product.ProductId)];
            var productName = TempData[nameof(Product.ProductName)];
            //var productName = TempData.Peek(nameof(Product.ProductName));

            var productAdded = _rs.ProductAdded; // Se marca eliminar
            productAdded = _rs.ProductAdded;
            TempData.Keep(nameof(_rs.ProductAdded));

            //var obj = TempData.Peek(nameof(_rs.ProductAdded));
            //TempData.Keep();
            #endregion

            var startTime = HttpContext.Items["StartTime"];

            return View(_ss.Cart);
        }

        public ActionResult Add(int? id)
        {
            if (id.HasValue)
            {
                #region Session
                var p = _context.Products.Find(id);
                var cart = _ss.Cart;
                cart.Items.Add(p);

                _ss.Cart = cart;
                #endregion

                #region TempData
                TempData[nameof(Product.ProductId)] = p.ProductId;
                TempData[nameof(Product.ProductName)] = p.ProductName;

                _rs.ProductAdded = p;
                #endregion
            }

            return RedirectToAction("Index");
        }
    }
}
