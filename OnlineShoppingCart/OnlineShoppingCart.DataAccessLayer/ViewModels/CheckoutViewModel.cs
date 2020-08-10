using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShoppingCart.DataAccessLayer.ViewModels
{
    public class CheckoutViewModel
    {
        public int CustomerId { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }

        public List<SelectedListViewModel> selectedListViewModel { get; set; }

    }




}
