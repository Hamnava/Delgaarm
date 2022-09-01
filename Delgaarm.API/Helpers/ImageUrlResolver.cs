using Application.ViewModels;
using Application.ViewModels.API.Dtos;
using AutoMapper;
using Infrastracture.Entities;
using Multipisus.Core.ViewModels.API.Dtos;


namespace Delgaarm.API.Helpers
{
    public class ImageUrlResolver
    {
    }

    // For EditUserDto Photo Url
    public class UserUrlResolver : IValueResolver<ApplicationUser, EditUserDto, string>
    {
        private readonly IConfiguration _config;
        public UserUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(ApplicationUser source, EditUserDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Profile))
            {
                return _config["ApiUrl"] + source.Profile;
            }

            return null;
        }
    }

    // For Slider Photo Url
    public class SliderUrlResolver : IValueResolver<Slider, SliderDto, string>
    {
        private readonly IConfiguration _config;
        public SliderUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Slider source, SliderDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.sliderImage))
            {
                return _config["ApiUrl"] + source.sliderImage;
            }

            return null;
        }
    }


    // Url for Product Image
    public class ProductUrlResolver : IValueResolver<Product, ProductlistDto, string>
    {
        private readonly IConfiguration _config;
        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Product source, ProductlistDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ProductImage))
            {
                return _config["ApiUrl"] + source.ProductImage;
            }

            return null;
        }
    }


    // For Service Image Url
    public class ServiceUrlResolver : IValueResolver<Service, ServiceDTo, string>
    {
        private readonly IConfiguration _config;
        public ServiceUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Service source, ServiceDTo destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ImageUrl))
            {
                return _config["ApiUrl"] + source.ImageUrl;
            }

            return null;
        }
    }



    // For Category Image Url
    public class CategoryUrlResolver : IValueResolver<Category, CategoryDto, string>
    {
        private readonly IConfiguration _config;
        public CategoryUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Category source, CategoryDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.CategoryPhoto))
            {
                return _config["ApiUrl"] + source.CategoryPhoto;
            }

            return null;
        }
    }



    

    // For Gallery Url
    public class GalleryUrlResolver : IValueResolver<ProductGallery, ProductGalleryDTO, string>
    {
        private readonly IConfiguration _config;
        public GalleryUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(ProductGallery source, ProductGalleryDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ImageUrl))
            {
                return _config["ApiUrl"] + source.ImageUrl;
            }

            return null;
        }
    }

  
}
