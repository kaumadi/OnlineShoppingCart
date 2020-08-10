using OnlineShoppingCart.DataAccessLayer.ViewModels;
using System.Collections.Generic;

namespace OnlineShoppingCart.BusinessLayer.IRepositories
{
    public interface IOrderService
    {
        List<ProductStockStatus> Checkout(CheckoutViewModel checkoutViewModel);
        void AddPurchase (PurchaseViewModel purchaseViewModel);
    }
}
