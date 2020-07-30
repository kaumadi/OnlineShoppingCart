using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShoppingCart.BusinessLayer.IRepositories;
using OnlineShoppingCart.DataAccessLayer.Contexts;
using OnlineShoppingCart.DataAccessLayer.Models;
using OnlineShoppingCart.DataAccessLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShoppingCart.BusinessLayer.Services
{
    public class OrderServices: IOrderService
    {
        readonly OnlineShoppingCartContext _shoppingcartContext;

        public OrderServices(OnlineShoppingCartContext context)
        {
            _shoppingcartContext = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void Add(Order order)
        {
            _shoppingcartContext.Orders.Add(order);
            _shoppingcartContext.SaveChanges();

        }


        public void AddOrderItems(OrderItem orderItem)
        {
            _shoppingcartContext.OrderItems.Add(orderItem);
            _shoppingcartContext.SaveChanges();

        }


        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(CheckoutViewModel[] checkoutViewModel)
        {
            var products = _shoppingcartContext.Products.AsQueryable();

            foreach (var orp in checkoutViewModel)
                  {
                    products = _shoppingcartContext.Products.Where(i => i.UnitsInStock > orp.availableQty && i.ProductId == orp.productid);
               }
               // products = _shoppingcartContext.Products.Where(i => i.UnitsInStock > checkoutViewModel.availableQty && i.ProductId== checkoutViewModel.productid);

            return await products.ToListAsync();
          // return await _shoppingcartContext.Products.ToListAsync(); 
        }

    }
}
