using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;

namespace WA1._1
{
    public class MyMiddleware
    {

        private readonly RequestDelegate _next;
        public MyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.Headers.Add("X-MyMiddleware", new StringValues("Have a nice day! :-)"));
            await _next.Invoke(context);
        }
    }
}
