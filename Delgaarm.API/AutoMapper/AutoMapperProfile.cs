using Application.ViewModels;
using Application.ViewModels.API.Dtos;
using AutoMapper;
using Delgaarm.API.Helpers;
using Infrastracture.Entities;



namespace Delgaarm.API.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserRegisterDTO, ApplicationUser>().ReverseMap();

            CreateMap<ApplicationUser, EditUserDto>().ReverseMap();
            CreateMap<ApplicationUser, EditUserDto>()
                 .ForMember(d => d.Profile, o => o.MapFrom<UserUrlResolver>());

            CreateMap<Slider, SliderDto>()
               .ForMember(d => d.sliderImage, o => o.MapFrom<SliderUrlResolver>());

            CreateMap<Product, ProductlistDto>()
              .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.Categories.CategoryName))
              .ForMember(d => d.ColorName, o => o.MapFrom(s => s.productColor.ColorName))
              .ForMember(d => d.ColorCode, o => o.MapFrom(s => s.productColor.ColorCode))
              .ForMember(d => d.ProductImage, o => o.MapFrom<ProductUrlResolver>());

            CreateMap<ProductGallery, ProductGalleryDTO>()
             .ForMember(d => d.ImageUrl, o => o.MapFrom<GalleryUrlResolver>());

            CreateMap<Service, ServiceDTo>()
              .ForMember(d => d.ImageUrl, o => o.MapFrom<ServiceUrlResolver>());

            CreateMap<ContactUs, ContactDto>().ReverseMap();

            CreateMap<Category, CategoryDto>()
             .ForMember(d => d.CategoryPhoto, o => o.MapFrom<CategoryUrlResolver>());

         
        }
    }
}
