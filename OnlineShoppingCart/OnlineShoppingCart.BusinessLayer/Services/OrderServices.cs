using Microsoft.EntityFrameworkCore;
using OnlineShoppingCart.BusinessLayer.IRepositories;
using OnlineShoppingCart.DataAccessLayer.Contexts;
using OnlineShoppingCart.DataAccessLayer.Models;
using OnlineShoppingCart.DataAccessLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShoppingCart.BusinessLayer.Services
{
    public class OrderServices : IOrderService
    {
        #region private propeties
        readonly OnlineShoppingCartContext _shoppingcartContext;
        #endregion

        #region Constructor
        public OrderServices(OnlineShoppingCartContext context)

        {
            _shoppingcartContext = context ?? throw new ArgumentNullException(nameof(context));
        }
        #endregion

        #region Checkout 
        public List<ProductStockStatus> Checkout(CheckoutViewModel checkoutViewModel)
        {
            var products = _shoppingcartContext.Products.ToList().AsQueryable();

            List<ProductStockStatus> FinalOut = new List<ProductStockStatus>();
            foreach (var checkout in checkoutViewModel.selectedListViewModel)
            {
                ProductStockStatus oneStatus = new ProductStockStatus
                {
                    ProductId = checkout.ProductId,
                    OrderdQty = checkout.OrderdQty,
                    ProductName = checkout.ProductName,
                    UnitPrice = checkout.UnitPrice,
                    ProductCurrentStatus = _shoppingcartContext.Products.Where(i => i.UnitsInStock >= checkout.OrderdQty && i.ProductId == checkout.ProductId).Any()
                };

                FinalOut.Add(oneStatus);
            }
            // products = _shoppingcartContext.Products.Where(i => i.UnitsInStock > checkoutViewModel.availableQty && i.ProductId== checkoutViewModel.productid);

            return FinalOut.ToList();
            // return await _shoppingcartContext.Products.ToListAsync(); 
        }
        #endregion

        #region Payment
        public void AddPurchase(PurchaseViewModel purchaseViewModel)
        {
            var order = new Order();
            {
                order.TotalAmount = purchaseViewModel.TotalAmount;
                order.OrderDate = purchaseViewModel.OrderDate;
                order.Customers = _shoppingcartContext.Customers.First(c => c.CustomerId == purchaseViewModel.CustomerId);
                _shoppingcartContext.Orders.Add(order);
                _shoppingcartContext.SaveChanges();
            }
            int orderID = order.OrderId;

            foreach (var purchase in purchaseViewModel.selectedListViewModel)
            {
                OrderItem orderItem = new OrderItem();
                {

                    orderItem.UnitPrice = purchase.UnitPrice;
                    orderItem.Quantity = purchase.OrderdQty;
                    orderItem.TotalAmount = purchase.TotalPrice;
                    orderItem.Orders = _shoppingcartContext.Orders.First(c => c.OrderId == orderID);
                    _shoppingcartContext.OrderItems.Add(orderItem);
                    _shoppingcartContext.SaveChanges();

                };
                int orderItemId = orderItem.OrderItemId;

                OrderItemProduct orderItemProduct = new OrderItemProduct();
                {
                    orderItemProduct.OrderItems = _shoppingcartContext.OrderItems.First(c => c.OrderItemId == orderItemId);
                    orderItemProduct.Products = _shoppingcartContext.Products.First(c => c.ProductId == purchase.ProductId);
                    _shoppingcartContext.SaveChanges();
                }

                Product product = new Product();
                {
                    
                    bool stock=_shoppingcartContext.Products.Where(i=>i.ProductId==purchase.ProductId).Any();
                    if (stock == true) {
                        product.UnitsInStock  -= purchase.OrderdQty;
                        _shoppingcartContext.SaveChanges();
                    }
                    
                }



                Payment payment = new Payment();
                {
                    payment.PaymentType = purchaseViewModel.PaymentType;
                    payment.Orders = _shoppingcartContext.Orders.First(c => c.OrderId == orderID);
                    _shoppingcartContext.Payments.Add(payment);
                    _shoppingcartContext.SaveChanges();
                }               
            }
        }
        #endregion

        #region GetOrderDetailsAsync
        public  async Task<List<OrderDetailsViewModel>> GetOrderDetailsAsync()
        {

            

           // List<OrderItem> orderItems = await _shoppingcartContext.OrderItems.ToListAsync();

            var ap = await (from p in _shoppingcartContext.Orders
                            join q in _shoppingcartContext.OrderItems on p.OrderId equals q.Orders.OrderId
                            join e in _shoppingcartContext.Payments on p.OrderId equals e.Orders.OrderId
                            select new OrderDetailsViewModel
                            {
                                OrderId = p.OrderId,
                                OrderDate = p.OrderDate 
                            }).ToListAsync();


            return ap; 
        }
        #endregion

    }
}
