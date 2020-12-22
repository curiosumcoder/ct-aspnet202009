using AngleSharp.Html.Dom;
using WA90.Tests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using System;

namespace WA90.Tests
{
    public class Web_Integration_Tests : IClassFixture<WebApplicationFactory<WA90.Startup>>
    {
        private readonly WebApplicationFactory<WA90.Startup> _factory;

        public Web_Integration_Tests(WebApplicationFactory<WA90.Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Home/Index")]
        [InlineData("/Home/Privacy")]
        public async Task Home_Action_RetornaContentTypeCorrecto(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task Home_Index_ContieneLista()
        {
            // Arrange
            var client = _factory.CreateClient();
            var defaultPage = await client.GetAsync("/");
            var content = await HtmlHelpers.GetDocumentAsync(defaultPage);

            // Act
            var liElement = (IHtmlUnorderedListElement)content.QuerySelector("main ul");

            // Assert
            Assert.True(liElement.Children.Length == 10);
            //Assert.Throws<InvalidOperationException>(() => Int32.Parse("123"));
        }
    }
}
