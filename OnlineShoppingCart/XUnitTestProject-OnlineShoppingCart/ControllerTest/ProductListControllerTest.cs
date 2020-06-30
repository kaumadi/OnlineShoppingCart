using Moq;
using OnlineShoppingCart.BusinessLayer.IRepositories;
using OnlineShoppingCart.DataAccessLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject_OnlineShoppingCart.ControllerTest
{
 
    [Fact]
    public async Task ProductsGetFromMoq()
    {
        // Arrange
        var serviceMock = new Mock<IProductRepository<Product>>();
        serviceMock.Setup(x => x.GetAllProductsAsync()).Returns(() => new List<Product>
  {
    new Product{ProductId=1,ProductName="Cake",UnitPrice=2000,UnitsInStock=2, Description="", ImagePath=""},
    new Product{ProductId=2,ProductName="Cake",UnitPrice=2000,UnitsInStock=2, Description="", ImagePath=""},
     new Product{ProductId=3,ProductName="Cake",UnitPrice=2000,UnitsInStock=2, Description="", ImagePath=""}
  }) ;
        var controller = new ProductController(serviceMock.Object);

        // Act
        var result = await controller.Get();

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        var persons = okResult.Value.Should().BeAssignableTo<IEnumerable<Person>>().Subject;

        persons.Count().Should().Be(3);
    }
}
