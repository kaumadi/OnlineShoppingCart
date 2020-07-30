using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShoppingCart.BusinessLayer.IRepositories;
using OnlineShoppingCart.BusinessLayer.Services;
using OnlineShoppingCart.DataAccessLayer.Models;
using OnlineShoppingCart.DataAccessLayer.ViewModels;

namespace OnlineShoppingCart.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        #region private fields
        private readonly IOrderService _orderService;
        #endregion

        #region Constructor
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(_orderService)); ;
        }
        #endregion


        // POST: api/Order
        [HttpPost("order")]
        public ActionResult PostOrderDetails([FromBody]Order order)
        {
            if (order == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }     
            _orderService?.Add(order);
            return Ok();         
        }

        // POST: api/Order
        [HttpPost("orderitems")]
        public ActionResult PostOrderItemDetails([FromBody]OrderItem orderItem)
        {
            if (orderItem == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _orderService?.AddOrderItems(orderItem);
            return Ok();
        }

        [HttpPost("checkout")]
        public async Task<ActionResult> PostOrderItem([FromBody]CheckoutViewModel[]  checkoutViewModel)
        {
            if (checkoutViewModel == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var results = await  _orderService?.GetProducts(checkoutViewModel);
            return Ok(results);
         
        }
    }
}