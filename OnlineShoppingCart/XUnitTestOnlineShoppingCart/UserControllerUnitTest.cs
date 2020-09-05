using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineShoppingCart.BusinessLayer.IRepositories;
using OnlineShoppingCart.Controllers;
using OnlineShoppingCart.DataAccessLayer.Models;
using OnlineShoppingCart.DataAccessLayer.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestOnlineShoppingCart
{
    public class UserControllerUnitTest
    {


        [Fact]
        public void User_registration_Check_Ok_Result_output()
        {
            // Arrange
            var serviceMock = new Mock<IUserService>();


            Customer customer = new Customer { };
            RegisterViewModel registrationViewModel = new RegisterViewModel { FirstName = "Test", LastName = "Test", Address = "Test", Contact = "Test", Email = "Test", UserName = "Test", Password = "Test" };
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<RegisterViewModel, Customer>(It.IsAny<RegisterViewModel>())).Returns(new Customer());
            serviceMock.Setup(x => x.Create(customer, registrationViewModel.Password)).Returns(new Customer());

            var controller = new UserController(serviceMock.Object, mapperMock.Object);

            // Act
            var result = controller.Register(registrationViewModel);

            // Assert
            var okResult = result.Should().BeOfType<OkResult>().Subject;
        }


        [Fact]
        public void Check_Register_View_Model_Is_Not_Empty()
        {
            // Arrange
            var serviceMock = new Mock<IUserService>();


            Customer customer = new Customer { };
            RegisterViewModel registrationViewModel = new RegisterViewModel();
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<Customer, RegisterViewModel>(It.IsAny<Customer>())).Returns(new RegisterViewModel() { FirstName = "Test", LastName = "Test", Address = "Test", Contact = "Test", Email = "Test", UserName = "Test", Password = "Test" });
     

            var controller = new UserController(serviceMock.Object, mapperMock.Object);

            serviceMock.Setup(x => x.Create(customer, registrationViewModel.Password)).Returns(new Customer());

            // Act
            var result = controller.Register(registrationViewModel);

            // Assert
            Assert.True(result != null, "Unexpected null result");

            //    var retrievedPostResult = result as OkResult<Post>;
            var okResult = result.Should().BeOfType<OkResult>().Subject;
            Assert.True(okResult != null, "Unexpected null retrievedPost");

           // Assert.Equal( null, customer.FirstName);
            
        }
       
    }

}


