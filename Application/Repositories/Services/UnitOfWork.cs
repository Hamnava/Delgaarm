using Application.Classes;
using Application.Repositories.Interfaces;
using Infrastracture.Context;
using Infrastracture.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.Services
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly ApplicationContext _context;
        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }
        private GenericClass<ApplicationUser> _usermanager;
        private GenericClass<Product> _product;
        private GenericClass<ProductGallery> _gallery;
        private GenericClass<Category> _category;
        private GenericClass<ContactUs> _contactUs;
        private GenericClass<YourDesign> _youDesign;
        private GenericClass<ProductColor> _productcolor;
        private GenericClass<Service> _service;
        private GenericClass<TailorSize> _tailorSize;
        private GenericClass<Slider> _slider;
        private GenericClass<Order> _order;
        private GenericClass<EmailRegister> _Subscribe;

        //social
        public GenericClass<EmailRegister> Subscribe
        {
            get
            {
                if (_Subscribe == null)
                {
                    _Subscribe = new GenericClass<EmailRegister>(_context);
                }
                return _Subscribe;
            }
        }


       

       

        //order
        public GenericClass<Order> OrderUW
        {
            get
            {
                if (_order == null)
                {
                    _order = new GenericClass<Order>(_context);
                }
                return _order;
            }
        }
        //slider
        public GenericClass<Slider> SliderUW
        {
            get
            {
                if (_slider == null)
                {
                    _slider = new GenericClass<Slider>(_context);
                }
                return _slider;
            }
        }
        //for user
        public GenericClass<ApplicationUser> UsermanagerUW
        {
            get
            {
                if (_usermanager == null)
                {
                    _usermanager = new GenericClass<ApplicationUser>(_context);
                }
                return _usermanager;
            }
        }
        //for product
        public GenericClass<Product> productUW
        {
            get
            {
                if (_product == null)
                {
                    _product = new GenericClass<Product>(_context);
                }
                return _product;
            }
        }
        //for productgallery
        public GenericClass<ProductGallery> productGalleryUW
        {
            get
            {
                if (_gallery == null)
                {
                    _gallery = new GenericClass<ProductGallery>(_context);
                }
                return _gallery;
            }
        }
        //for Category
        public GenericClass<Category> CategoryUW
        {
            get
            {
                if (_category == null)
                {
                    _category = new GenericClass<Category>(_context);
                }
                return _category;
            }
        }

        //for contact us
        public GenericClass<ContactUs> ContactUsUW
        {
            get
            {
                if (_contactUs == null)
                {
                    _contactUs = new GenericClass<ContactUs>(_context);
                }
                return _contactUs;
            }
        }
        //for ABOUT
        public GenericClass<YourDesign> YourDesignUW
        {
            get
            {
                if (_youDesign == null)
                {
                    _youDesign = new GenericClass<YourDesign>(_context);
                }
                return _youDesign;
            }
        }
        //for productcolor
        public GenericClass<ProductColor> ProductColorUW
        {
            get
            {
                if (_productcolor == null)
                {
                    _productcolor = new GenericClass<ProductColor>(_context);
                }
                return _productcolor;
            }
        }

        //for service
        public GenericClass<Service> ServiceUW
        {
            get
            {
                if (_service == null)
                {
                    _service = new GenericClass<Service>(_context);
                }
                return _service;
            }
        }

        //for team
        public GenericClass<TailorSize> TialorSizeUW
        {
            get
            {
                if (_tailorSize == null)
                {
                    _tailorSize = new GenericClass<TailorSize>(_context);
                }
                return _tailorSize;
            }
        }

        public async Task saveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
