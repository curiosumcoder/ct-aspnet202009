using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Model;

namespace WA30.Pages
{
    public class ClientCSSModel : PageModel
    {
        public List<IGrouping<string, Product>> Products { get; set; }

        [BindProperty()]
        public string Filter { get; set; }
        public void OnGet() 
        { 
        }
        public void OnPost()
        {
            var pD = new ProductD();
            var pM = pD.List();

            // LINQ
            Products = pM.Where(p => p.ProductName.Contains(Filter ?? "",
                System.StringComparison.InvariantCultureIgnoreCase)).GroupBy(p => p.CategoryName).ToList();
        }
    }
}
