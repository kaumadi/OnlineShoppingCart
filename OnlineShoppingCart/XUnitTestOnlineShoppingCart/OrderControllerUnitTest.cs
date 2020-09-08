using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineShoppingCart.BusinessLayer.IRepositories;
using OnlineShoppingCart.Controllers;
using OnlineShoppingCart.DataAccessLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestOnlineShoppingCart
{
    public class OrderControllerUnitTest
    {
        #region Checkout Method
        /// <summary>
        /// Unit Test - Checkout Method
        /// Test Case 1- Test Service output is not null
        /// Test Case 2- Test Controller retrive all output product count in array
        /// Test Case 3- Test whether user given input list values are return in return array
        /// </summary>
        [Fact]
        public void Checkout_Test_Return_Object_IsValid()
        {
            // Arrange
            var serviceMock = new Mock<IOrderService>();

            CheckoutViewModel checkoutViewModel = new CheckoutViewModel() {
                CustomerId = 1,
                Token = "asdhjdsldmk1192j",
                UserName = "TestUser",
                selectedListViewModel = new List<SelectedListViewModel>{
                new SelectedListViewModel {ProductId=1,ProductName="TestProduct1",OrderdQty=2,UnitPrice=1500,TotalPrice=3000},
                new SelectedListViewModel {ProductId=2,ProductName="TestProduct1",OrderdQty=2,UnitPrice=1500,TotalPrice=3000},
                new SelectedListViewModel {ProductId=3,ProductName="TestProduct1",OrderdQty=2,UnitPrice=1500,TotalPrice=3000},
                }
            };
            serviceMock.Setup(x => x.Checkout(checkoutViewModel)).Returns(new List<ProductStockStatus> {
            new ProductStockStatus{ProductId=1,ProductName="TestProduct1",OrderdQty=2,UnitPrice=1200,ProductCurrentStatus=true},
            new ProductStockStatus{ProductId=2,ProductName="TestProduct1",OrderdQty=2,UnitPrice=1200,ProductCurrentStatus=false},
            new ProductStockStatus{ProductId=3,ProductName="TestProduct1",OrderdQty=2,UnitPrice=1200,ProductCurrentStatus=true},
            });

            var controller = new OrderController(serviceMock.Object);

            // Act
            var result = controller.Checkout(checkoutViewModel);

            // Assert


            //Test Get Return Object Null or Not
            //Assert.True(result != null, "Unexpected null result");
            Assert.NotNull(result);

 
            //Test Controller return Ok Result
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

            //Test return all given products
            var products = okResult.Value.Should().BeAssignableTo<List<ProductStockStatus>>().Subject;
            products.Count.Should().Be(3);
            Assert.Equal(checkoutViewModel.selectedListViewModel.Count,products.Count);
          
           
        }
        #endregion 

        #region Get Order Details Method
        /// <summary>
        /// Test service moq and get output model by Order Id 
        /// </summary>
        /// <returns>Test Output</returns>
        [Fact]
        public async Task OrderDetails_Test_OrderDetails_By_OrderId()
        {
            // Arrange
            var serviceMock = new Mock<IOrderService>();
            int orderId = 1;
                    
            serviceMock.Setup(x => x.GetOrderDetailsAsync(orderId)).ReturnsAsync(() => new OrderDetailsViewModel() { 
                    OrderId = 1 ,
                    CustomerId=1,
                    FirstName="Test1",
                    LastName="Test2",
                    Address="address1",
                    Contact="Test1",
                  // OrderDate= "5/1/2008 8:30:52 AM",
                   orderItemsViewModel=new List<OrderItemsViewModel>() { 
                   new OrderItemsViewModel() { OrderItemId=1,ProductName="TestProduct",ImagePath="TestImage",Quantity=1,UnitPrice=1200},
                   new OrderItemsViewModel() { OrderItemId=2,ProductName="TestProduct",ImagePath="TestImage",Quantity=1,UnitPrice=1200}},
                   PaymentMethod="cash",
                   TotalAmount=20000
                    }) ;

            var controller = new OrderController(serviceMock.Object);

            // Act
            var result = await controller.GetOrderDetailsAsync(orderId);

            // Assert
            //First, assert that the Order details returned is not Empty.
            Assert.NotNull(result);
        
            //Test return Ok result
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

            //Test Return details by given Order Id or not
            var orderDetails = okResult.Value.Should().BeAssignableTo<OrderDetailsViewModel>().Subject;
            orderDetails.OrderId.Should().Be(1);
        }

        #endregion 

        #region Get Payment History Method
        [Fact]
        public async Task PaymentHistory_Test_OrderDetails_By_CustomerId()
        {
            // Arrange
            var serviceMock = new Mock<IOrderService>();
            int customerId = 1;
           // IEnumerable<PaymentHistoryViewModel> plist = new List<PaymentHistoryViewModel> { };
            serviceMock.Setup(x => x.GetAllPaymentsAsync(customerId)).ReturnsAsync(() => new List<PaymentHistoryViewModel>
            {
                new PaymentHistoryViewModel{OrderId=1,OrderDate=DateTime.Parse("2/8/2020"),TotalAmount=20000 },
                new PaymentHistoryViewModel{OrderId=2,OrderDate=DateTime.Parse("2/9/2020"),TotalAmount=10000 },
                new PaymentHistoryViewModel{OrderId=3,OrderDate=DateTime.Parse("7/9/2020"),TotalAmount=40000 },
                new PaymentHistoryViewModel{OrderId=4,OrderDate=DateTime.Parse("7/9/2020"),TotalAmount=40000 }
            });

            var controller = new OrderController(serviceMock.Object);

            // Act
            var result = await controller.GetOrderDetailsAsync(customerId);

            // Assert

            //First, assert that the Order details returned is not Empty.
            Assert.NotNull(result);

            //Test return Ok result
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

            //Test all payment history order counts return 
          // var paymentHistory = okResult.Value.Should().BeAssignableTo<IEnumerable<PaymentHistoryViewModel>>().Subject;
          //  paymentHistory.Count().Should().Be(4);
            //  paymentHistory.Should().NotBeEmpty();

        }
        #endregion

        #region Payment Add Method
        [Fact]
        public void PaymentHistory_Test_OutputValue()
        {
            //Arrange 
            var serviceMock = new Mock<IOrderService>();
            PurchaseViewModel purchaseViewModel = new PurchaseViewModel();
            int orderId=1;
            serviceMock.Setup(x => x.AddPurchase(purchaseViewModel)).Returns(() => orderId);

            var controller = new OrderController(serviceMock.Object);

            // Act
            var resultId = controller.Payment(purchaseViewModel);

            //Assert

            //Test result from service is not null
            Assert.NotNull(resultId);

            //Test return Ok result
            var okResult = resultId.Should().BeOfType<OkObjectResult>().Subject;

            //Test return orderId is expected orderId
            var payment = okResult.Value.Should().Be(1);
            var output = okResult.Value.Should().NotBeNull();
            var outputType = okResult.Value.Should().BeOfType<Int32>();

        }
        #endregion

    }



}
