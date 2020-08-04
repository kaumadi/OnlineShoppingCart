using OnlineShoppingCart.DataAccessLayer.ViewModels;
using System.Collections.Generic;

namespace OnlineShoppingCart.BusinessLayer.IRepositories
{
    public interface IOrderService
    {
        List<ProductStockStatus> Checkout(List<CheckoutViewModel> checkoutViewModel);
        void AddPurchase (PurchaseViewModel purchaseViewModel);
    }
}
