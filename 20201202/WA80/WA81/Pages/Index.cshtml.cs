using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WA81.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async void OnGet()
        {
            //var client = new swaggerClient("https://localhost:44374", new System.Net.Http.HttpClient());

            //var result = await client.SearchAsync("queso");

            var c = new MyNamespace.Client("https://localhost:44374", new System.Net.Http.HttpClient());
            var result = await c.SearchAsync("queso");

        }
    }
}
