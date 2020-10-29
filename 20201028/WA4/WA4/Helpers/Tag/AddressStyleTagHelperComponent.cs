using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Threading.Tasks;

namespace WA4.Helpers.Tag
{
    public class AddressStyleTagHelperComponent : TagHelperComponent
    {
        private readonly string _style = @"<link rel='stylesheet' href='/css/demo-tag-helper-component.css' />";

        public override int Order => 1;

        public override Task ProcessAsync(TagHelperContext context,
                                          TagHelperOutput output)
        {
            if (string.Equals(context?.TagName, "head",
                              StringComparison.OrdinalIgnoreCase))
            {
                output?.PostContent.AppendHtml(_style);
            }

            return Task.CompletedTask;
        }
    }
}
