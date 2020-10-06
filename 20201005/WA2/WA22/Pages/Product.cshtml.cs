using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pato.Conta.Data;
using Pato.Conta.Model;

namespace WA22.Pages
{
    public class ProductModel : PageModel
    {
        public List<Product> Products { get; set; }

        /// <summary>
        /// GET /Product
        /// </summary>
        public void OnGet()
        {
            ProductD pD = new ProductD();

            Products = pD.GetList();
        }
    }
}
