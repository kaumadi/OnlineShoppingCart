using Microsoft.AspNetCore.Mvc;
using OnlineShoppingCart.DataAccessLayer.Models;
using OnlineShoppingCart.DataAccessLayer.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShoppingCart.BusinessLayer.IRepositories
{
    public interface IOrderService
    {
        void Add(Order order);
        void AddOrderItems(OrderItem orderItem);
        Task<ActionResult<IEnumerable<Product>>> GetProducts(CheckoutViewModel[] checkoutViewModel);
    }
}
