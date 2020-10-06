using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WA22.Pages
{
    public class ProductModel0 : PageModel
    {
        public IEnumerable<int> Nums { get; set; } = Enumerable.Range(1, 20);

        public void OnGet()
        {
        }
    }
}
