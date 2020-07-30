using AutoMapper;
using OnlineShoppingCart.DataAccessLayer.Models;
using OnlineShoppingCart.DataAccessLayer.ViewModels;

namespace OnlineShoppingCart.DataAccessLayer.Mappings
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Customer, CustomerViewModel>();
            CreateMap<RegisterViewModel, Customer>().ReverseMap();
        }

            

    }
}
