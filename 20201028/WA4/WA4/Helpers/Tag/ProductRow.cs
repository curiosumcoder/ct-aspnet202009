using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Northwind.Store.Model;
using System;

namespace WA4.Helpers.Tag
{
    [HtmlTargetElement("tr", Attributes = DataAttrName )]
    public class ProductRow : TagHelper
    {
        private const string DataAttrName = "product";

        [HtmlAttributeName(DataAttrName)]
        public Product Data { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (output != null)
            {
                output.TagMode = TagMode.StartTagAndEndTag;

                if (Data == null)
                {
                    throw new InvalidOperationException("Datos del producto requeridos.");
                }
                else
                {
                    var td = new TagBuilder("td");
                    td.InnerHtml.SetContent(Data.ProductName);
                    output.Content.AppendHtml(td);

                    td = new TagBuilder("td");
                    td.InnerHtml.SetContent(Data.QuantityPerUnit);
                    output.Content.AppendHtml(td);

                    td = new TagBuilder("td");
                    td.InnerHtml.SetContent(Data.UnitPrice.ToString());
                    output.Content.AppendHtml(td);

                    td = new TagBuilder("td");
                    td.InnerHtml.SetContent(Data.UnitsInStock.ToString());
                    output.Content.AppendHtml(td);

                    //output.Attributes.SetAttribute("class", "bg-primary");
                }

            }
        }
    }
}
