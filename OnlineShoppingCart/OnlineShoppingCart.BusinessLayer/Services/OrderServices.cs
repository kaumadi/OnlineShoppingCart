using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
        public int AddPurchase(PurchaseViewModel purchaseViewModel)
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
                    _shoppingcartContext.OrderItemProducts.Add(orderItemProduct);
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

            foreach (var orderItem in purchaseViewModel.selectedListViewModel)
            {
                Product product = new Product();
                var orderItemToUpdate = _shoppingcartContext.Products.FirstOrDefault(c => c.ProductId == orderItem.ProductId);
                orderItemToUpdate.UnitsInStock -= orderItem.OrderdQty;
                product.UnitsInStock = orderItemToUpdate.UnitsInStock;
                _shoppingcartContext.Entry(product).CurrentValues.SetValues(orderItemToUpdate);
                _shoppingcartContext.SaveChanges();
            }

            return orderID;

        }
        #endregion

        #region GetOrderDetailsAsync
        public async Task<OrderDetailsViewModel> GetOrderDetailsAsync(int orderId)
        {
            OrderDetailsViewModel query = await _shoppingcartContext.Orders.
            Join(_shoppingcartContext.Customers, order => order.Customers.CustomerId, customer => customer.CustomerId,
            (order, customer) => new { order, customer })
           .Join(_shoppingcartContext.Payments, orderPayment => orderPayment.order.OrderId, payment => payment.Orders.OrderId, (orderPayment, payment) => new { orderPayment, payment })
            .Join(_shoppingcartContext.OrderItems, itemsorder => itemsorder.orderPayment.order.OrderId, ordeItem => ordeItem.Orders.OrderId, (itemsorder, ordeItem) => new { itemsorder, ordeItem })
            .Where(m => m.itemsorder.orderPayment.order.OrderId == orderId)
            .Select(m => new OrderDetailsViewModel
            {
                OrderId = m.itemsorder.orderPayment.order.OrderId,
                OrderDate = m.itemsorder.orderPayment.order.OrderDate,
                TotalAmount = m.itemsorder.orderPayment.order.TotalAmount,
                CustomerId = m.itemsorder.orderPayment.order.Customers.CustomerId,
                FirstName = m.itemsorder.orderPayment.order.Customers.FirstName,
                LastName = m.itemsorder.orderPayment.order.Customers.LastName,
                Address = m.itemsorder.orderPayment.order.Customers.Address,
                Contact = m.itemsorder.orderPayment.order.Customers.Contact,
                PaymentMethod = m.itemsorder.payment.PaymentType,

            }).FirstOrDefaultAsync(c => c.OrderId==orderId);
           // var orderdetailslist = _shoppingcartContext.OrderItems.Where(oid => oid.Orders.OrderId == orderId).ToList();
            var orderdetailslist = _shoppingcartContext.OrderItems
                .Join(_shoppingcartContext.OrderItemProducts, oip => oip.OrderItemId, oi => oi.OrderItems.OrderItemId,
            (oip, oi) => new { oip, oi })
                .Where(oid => oid.oip.Orders.OrderId == orderId)
           .Select(s => new OrderItemsViewModel
            {
                OrderItemId = s.oip.OrderItemId,
                Quantity = s.oip.Quantity,
                UnitPrice = s.oip.UnitPrice,
                ProductName=s.oi.Products.ProductName,
                ImagePath=s.oi.Products.ImagePath
            }).ToList();
            query.orderItemsViewModel = orderdetailslist;

            return query;

        }
        #endregion

        #region Payment History
        public async Task<IEnumerable<PaymentHistoryViewModel>> GetAllPaymentsAsync(int customerId)
        {
            return await _shoppingcartContext.Orders.Where(oid => oid.Customers.CustomerId == customerId)
                .Select(c=>new PaymentHistoryViewModel
                { OrderId=c.OrderId,
                OrderDate=c.OrderDate,
                TotalAmount=c.TotalAmount
                }).ToListAsync();
        }
        #endregion
    }
}
