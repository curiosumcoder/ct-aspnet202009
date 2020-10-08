using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Data;
using Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace WA30.Pages
{
    public class ServerSideModel : PageModel
    {
        public List<Product> Products { get; set; }

        [BindProperty]
        public string Filter { get; set; }
        public void OnGet()
        {
            var pD = new ProductD();
            var pM = pD.List();

            Products = pM;
        }

        public void OnPost()
        {
            var pD = new ProductD();
            var pM = pD.List();

            // LINQ
            pM = pM.Where(p => p.ProductName.Contains(Filter)).ToList();

            Products = pM;
        }
    }
}
