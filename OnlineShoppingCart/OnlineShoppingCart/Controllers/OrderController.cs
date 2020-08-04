using Microsoft.AspNetCore.Mvc;
using OnlineShoppingCart.BusinessLayer.IRepositories;
using OnlineShoppingCart.DataAccessLayer.ViewModels;
using System;
using System.Collections.Generic;

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

        #region Checkout ActionResult

        /// <summary>
        /// Compaire Availability of stock and Orderd quantity 
        /// </summary>
        /// <param name="List<CheckoutViewModel>"></param>
        /// <returns></returns>

        [HttpPost("Checkout")]
        public ActionResult Checkout([FromBody]List<CheckoutViewModel> checkoutViewModel)
        {
            if (checkoutViewModel == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var results = _orderService?.Checkout(checkoutViewModel);
            return Ok(results);        
        }
        #endregion Checkout ActionResult

        #region PurchaseItem ActionResult
        
        /// <summary>
        /// POST : api/Order
        /// </summary>
        /// <param name="purchaseViewModel"></param>
        /// <returns></returns>
        [HttpPost("Purchase")]
        public ActionResult PurchaseItem([FromBody]PurchaseViewModel purchaseViewModel)
        {
            if (purchaseViewModel == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _orderService?.AddPurchase(purchaseViewModel);
            return Ok();
        }

        #endregion PurchaseItem ActionResult
    }
}