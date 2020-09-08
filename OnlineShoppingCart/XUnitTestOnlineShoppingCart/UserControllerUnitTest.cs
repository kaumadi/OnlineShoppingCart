using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Moq;
using OnlineShoppingCart.BusinessLayer.IRepositories;
using OnlineShoppingCart.Controllers;
using OnlineShoppingCart.DataAccessLayer.Models;
using OnlineShoppingCart.DataAccessLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestOnlineShoppingCart
{
    public class UserControllerUnitTest
    {
        #region User Registration Method

        [Fact]
        public void User_registration_Check_Ok_Result_output()
        {
            // Arrange
            var serviceMock = new Mock<IUserService>();


            Customer customer = new Customer{
                FirstName = "Test",
                LastName = "Test",
                Address = "Test",
                Contact = "Test",
                Email = "Test",
                UserName = "Test",
            };

            RegisterViewModel registrationViewModel = new RegisterViewModel
            {
                FirstName = "Test",
                LastName = "Test",
                Address = "Test",
                Contact = "Test",
                Email = "Test",
                UserName = "Test",
            };
           
            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(m => m.Map<RegisterViewModel, Customer>(It.IsAny<RegisterViewModel>())).Returns(new Customer());

            serviceMock.Setup(x => x.Create(customer, registrationViewModel.Password)).Returns(new Customer
            {
                    CustomerId= 2003,
                    FirstName= "Jason",
                    LastName= "Watmore",
                    Address= "test",
                    Email= "test",
                    Contact= "test",
                    UserName= "jason",
                    PasswordHash=new byte[] {101},
                    PasswordSalt= new byte[] {101},
                    Orders = null
            });

            var controller = new UserController(serviceMock.Object, mapperMock.Object);

            // Act
            var result = controller.Register(registrationViewModel);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

            // Assert.Equal(Customer,okResult.Value);
          //  var newCustomer = okResult.Value.Should().BeSameAs(customer);
         // newCustomer.CustomerId.Should().Be(1);
          //  newCustomer.Should().NotBeNull();
          //  newCustomer.UserName.Should().Equals(registrationViewModel.UserName);

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
        #endregion

        #region User Authenticate Method
        [Fact]
        public void UserLoginAuthentication_Test() {
        
            // Arrange
            var serviceMock = new Mock<IUserService>();
            var mapperMock = new Mock<IMapper>();
            AuthenticationViewModel authenticationViewModel = new AuthenticationViewModel();
            string userName = "jason";
            string password = "password";
            Customer customer = new Customer
            {
                CustomerId = 2003,
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "TestAddress",
                Email = "TestEmail",
                Contact = "TestContact",
                UserName = "jason"
            };

            serviceMock.Setup(x => x.Authenticate(userName, password)).Returns(customer);

            //var tokenHandlerMock= new Mock<JwtSecurityTokenHandler>();
            //var key = Encoding.ASCII.GetBytes("TestSecretKey");
          
            //var tokenDescriptor = new Mock<SecurityTokenDescriptor>();
            //tokenDescriptor.Setup(x=>x.)
            //var token = tokenHandler.CreateToken(tokenDescriptor);
            //var tokenString = tokenHandler.WriteToken(token);
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes("TestSecretKey");
            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new Claim[]
            //    {
            //        new Claim(ClaimTypes.Name, customer.CustomerId.ToString())
            //    }),
            //    Expires = DateTime.UtcNow.AddDays(7),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //};
            //var token = tokenHandler.CreateToken(tokenDescriptor);
            //var tokenString = tokenHandler.WriteToken(token);

            var controller = new UserController(serviceMock.Object,mapperMock.Object);

            // Act
            var result = controller.Authenticate(authenticationViewModel);


            //Assert

            Assert.NotNull(result);
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

            //Test all payment history order counts return 
           // var authenticate = okResult.Value.Should().BeAssignableTo<Customer>().Subject;
          //  authenticate.CustomerId.Should().Be(1);
        }
        #endregion

    }

}


