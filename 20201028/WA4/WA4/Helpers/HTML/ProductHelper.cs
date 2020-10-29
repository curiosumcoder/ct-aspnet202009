using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Northwind.Store.Model;
using System.IO;

namespace WA4.Helpers.HTML
{
    public static class ProductHelper
    {
        public static HtmlString ProductDetail(Product p)
        {
            HtmlString result = new HtmlString("");

            if (p != null)
            {
                result = new HtmlString($@"<tr>
                    <td>{p.ProductName}</td>
                    <td>{p.QuantityPerUnit}</td>
                    <td>{p.UnitPrice}</td>
                    <td>
                        <a href=""/Home/Details/{p.ProductId}"">Details</a>
                    </td></tr>");
            }

            return result;
        }

        public static HtmlString ProductDetail2(Product p)
        {
            HtmlString result = new HtmlString("");

            if (p != null)
            {
                TagBuilder tb = new TagBuilder("tr");

                TagBuilder tbProductName = new TagBuilder("td");
                tbProductName.InnerHtml.SetContent(p.ProductName);
                tb.InnerHtml.AppendHtml(tbProductName);

                TagBuilder tbQuantityUnit = new TagBuilder("td");
                tbQuantityUnit.InnerHtml.SetContent(p.QuantityPerUnit);
                tb.InnerHtml.AppendHtml(tbQuantityUnit);

                TagBuilder tbUnitPrice = new TagBuilder("td");
                tbUnitPrice.InnerHtml.SetContent(p.UnitPrice.ToString());
                tb.InnerHtml.AppendHtml(tbUnitPrice);

                TagBuilder td = new TagBuilder("td");

                TagBuilder tbLink = new TagBuilder("a");
                tbLink.MergeAttribute("href", $"/Home/Details/{p.ProductId}");
                tbLink.InnerHtml.SetContent("Details");

                td.InnerHtml.AppendHtml(tbLink);

                tb.InnerHtml.AppendHtml(td);

                using (var writer = new StringWriter())
                {
                    tb.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
                    result = new HtmlString(writer.ToString());
                }
            }

            return result;
        }
    }
}
