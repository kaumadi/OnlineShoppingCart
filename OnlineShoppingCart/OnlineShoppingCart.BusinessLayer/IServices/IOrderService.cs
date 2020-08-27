using OnlineShoppingCart.DataAccessLayer.Models;
using OnlineShoppingCart.DataAccessLayer.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShoppingCart.BusinessLayer.IRepositories
{
    public interface IOrderService
    {
        List<ProductStockStatus> Checkout(CheckoutViewModel checkoutViewModel);
        int AddPurchase (PurchaseViewModel purchaseViewModel);
        Task<OrderDetailsViewModel> GetOrderDetailsAsync(int orderId);
        Task<IEnumerable<PaymentHistoryViewModel>> GetAllPaymentsAsync(int customerId);
        void SendEmail(EmailMessage message);
    }
}
