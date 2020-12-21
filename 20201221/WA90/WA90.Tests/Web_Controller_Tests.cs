using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using WA90.Controllers;
using Xunit;

namespace WA90.Tests
{
    public class Web_Controller_Tests
    {
        //[Fact]
        //public void Index_Retorna_ListaDeNúmeros()
        //{
        //    // Arrange
        //    ILogger<HomeController> logger = logger = Mock.Of<ILogger<HomeController>>();

        //    var mockNum = new Mock<Data.IGetList<int>>();
        //    mockNum.Setup(repo => repo.GetList())
        //        .Returns(new List<int>() { 2, 46, 3, 6, 9, 2, 34, 67, 89, 202, 304 });

        //    var controller = new HomeController(logger, mockNum.Object);

        //    // Act
        //    var result = controller.Index();

        //    // Assert
        //    var viewResult = Assert.IsType<ViewResult>(result);
        //    var model = Assert.IsAssignableFrom<IEnumerable<int>>(viewResult.Model);
        //    Assert.Equal(11, model.Count());
        //}
    }
}
