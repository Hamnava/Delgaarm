using Application.ViewModels;
using AutoMapper;
using Infrastracture.Entities;

namespace Delgaarm.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
            CreateMap<ApplicationUser, ChangePasswordByAdminViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<ContactUs, ContuctUsViewModel>().ReverseMap();
            CreateMap<Service, ServiceViewModel>().ReverseMap();
            CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<YourDesign, YourDesignViewModel>().ReverseMap();
            CreateMap<TailorSize, TailorViewModel>().ReverseMap();
            CreateMap<Slider, SliderViewModel>().ReverseMap();

        }
    }
}
