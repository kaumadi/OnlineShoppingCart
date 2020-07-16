using AutoMapper;
using OnlineShoppingCart.DataAccessLayer.Models;


namespace OnlineShoppingCart.DataAccessLayer.Mappings
{
    public class ViewModelToEntityMappingProfile : Profile
    {
        public ViewModelToEntityMappingProfile()
        {
            CreateMap<RegistrationViewModel, AppUser>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));
        }
    }
}
