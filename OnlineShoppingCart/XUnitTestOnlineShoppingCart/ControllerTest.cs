using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineShoppingCart.BusinessLayer.IRepositories;
using OnlineShoppingCart.Controllers;
using OnlineShoppingCart.DataAccessLayer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestOnlineShoppingCart
{
    public class ControllerTest
    {
        [Fact]
        public async Task Get_All_Products_Count_Check_Using_Moq()
        {
            // Arrange
            var serviceMock = new Mock<IProductRepository>();

            IEnumerable<Product> plist = new List<Product> { };
            serviceMock.Setup(x => x.GetAllProductsAsync()).ReturnsAsync(() => new List<Product> {

             new Product{ProductId=1, ProductName="Cake", UnitPrice=1250,UnitsInStock=2,Description="Item 001",ImagePath="1.png"},
             new Product{ProductId=2, ProductName="Flowers", UnitPrice=1250,UnitsInStock=2,Description="Item 002",ImagePath="1.png"},
             new Product{ProductId=3, ProductName="Soft Toys", UnitPrice=1250,UnitsInStock=2,Description="Item 003",ImagePath="1.png"} });
            
            var controller = new ProductController (serviceMock.Object);

            // Act
            var result = await controller.GetAllProductsAsync();

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var persons = okResult.Value.Should().BeAssignableTo<IEnumerable<Product>>().Subject;

            persons.Count().Should().Be(3);
        }
    }
    
}
