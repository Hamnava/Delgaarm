using Application.Classes;
using Infrastracture.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        GenericClass<ApplicationUser> UsermanagerUW { get; }
        GenericClass<Product> productUW { get; }
        GenericClass<ProductGallery> productGalleryUW { get; }
        GenericClass<Category> CategoryUW { get; }
        GenericClass<ContactUs> ContactUsUW { get; }
        GenericClass<YourDesign> YourDesignUW { get; }
        GenericClass<ProductColor> ProductColorUW { get; }
        GenericClass<Service> ServiceUW { get; }
        GenericClass<TailorSize> TialorSizeUW { get; }
        GenericClass<Slider> SliderUW { get; }
        GenericClass<Order> OrderUW { get; }
      
        GenericClass<EmailRegister> Subscribe { get; }
        Task saveAsync();
        void Dispose();
    }
}
