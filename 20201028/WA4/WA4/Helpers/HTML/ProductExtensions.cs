using Microsoft.AspNetCore.Html;
using Northwind.Store.Model;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WA4.Helpers.HTML
{
    public static class ProductExtensions
    {
        public static HtmlString ProductDetail(this IHtmlHelper helper, Product p)
        {
            return ProductHelper.ProductDetail2(p);
        }
    }
}
