using Microsoft.AspNetCore.Mvc;
using SaleWebsite.Controllers;
namespace TestProject2
{
    public class UnitTest1
    {
        [Fact]
        public void Index_ReturnsViewResult()
        {
            
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}