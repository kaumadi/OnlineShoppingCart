using OnlineShoppingCart.BusinessLayer.IRepositories;
using OnlineShoppingCart.DataAccessLayer.Contexts;
using OnlineShoppingCart.DataAccessLayer.Models;
using OnlineShoppingCart.DataAccessLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OnlineShoppingCart.BusinessLayer.Services
{
    public class OrderServices: IOrderService
    {
        readonly OnlineShoppingCartContext _shoppingcartContext;

        public OrderServices(OnlineShoppingCartContext context)

        {
            _shoppingcartContext = context ?? throw new ArgumentNullException(nameof(context));
        }


        public List<ProductStockStatus> Checkout(List<CheckoutViewModel> checkoutViewModel)
        {
            var products = _shoppingcartContext.Products.ToList().AsQueryable();

            List<ProductStockStatus> FinalOut = new List<ProductStockStatus>();
            foreach (var checkout in checkoutViewModel)
            {
                ProductStockStatus oneStatus = new ProductStockStatus
                { 
                    ProductID = checkout.ProductId,
                    ProductCurrentStatus = _shoppingcartContext.Products.Where(i => i.UnitsInStock >= checkout.AvailableStockQty && i.ProductId == checkout.ProductId).Any()
                };

                FinalOut.Add(oneStatus);
            }
               // products = _shoppingcartContext.Products.Where(i => i.UnitsInStock > checkoutViewModel.availableQty && i.ProductId== checkoutViewModel.productid);

            return FinalOut;
          // return await _shoppingcartContext.Products.ToListAsync(); 
        }


        public void AddPurchase(PurchaseViewModel purchaseViewModel)
        {
            var order = new Order();
            {
                order.TotalAmount = purchaseViewModel.TotalAmount;
                order.Discount = purchaseViewModel.Discount;
                order.Customers= _shoppingcartContext.Customers.First(c => c.CustomerId == purchaseViewModel.CustomerId); 
                _shoppingcartContext.Orders.Add(order);
                _shoppingcartContext.SaveChanges();
            }
            int orderID = order.OrderId;
            var orderItem = new List<OrderItem>();
            {
                foreach (var oi in orderItem)
                {
                    oi.Quantity = purchaseViewModel.Quantity;
                    oi.Orders = _shoppingcartContext.Orders.First(c => c.OrderId == orderID);
                    _shoppingcartContext.Orders.Add(order);
                    _shoppingcartContext.SaveChanges();
                }
                }
            var payment = new Payment();
            {
                payment.PaymentType = purchaseViewModel.PaymentType;
                payment.Orders= _shoppingcartContext.Orders.First(c => c.OrderId == orderID);
                _shoppingcartContext.SaveChanges();
            }
        

        }

    }
}
