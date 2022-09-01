using Application.Repository.Interfaces;
using Infrastracture.Context;
using Infrastracture.Entities;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository.Services
{
    public class ProductService : IProductRepasitory
    {
        private readonly ApplicationContext _context;
        public ProductService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ProductGallery> GetGalleryByProductId(int productId)
        {
            return await _context.ProductGalleries.Where(x => x.ProductId == productId).SingleOrDefaultAsync();
        }

        public async Task<bool> GetProductByProductNameAsync(string productName, int id)
        {
            var product = await _context.products.FirstOrDefaultAsync(x => x.ProductName == productName);
            if (product != null && product.Id != id) return true;
            return false;
        }


        public List<Product> Search(string text, List<int> categoryid, int sort = 1)
        {
            IQueryable<Product> products = _context.products.Where(x => x.ProductName.Contains(text));

            switch (sort)
            {
                case 1:
                    products = products.OrderByDescending(p => p.IsBestSeller);
                    break;
                case 2:
                    products = products.OrderByDescending(p => p.IsNew);
                    break;


            }

            if (categoryid.Count() > 0)
            {
                products = products.Where(c => categoryid.Contains(c.CategoryId));
            }

            var query = (from p in products
                         select new
                         {
                             id = p.Id,
                             name = p.ProductName,
                             isNew = p.IsNew,
                             image = p.ProductImage
                         });

            List<Product> listProducts = new List<Product>();
            foreach (var product in products)
            {
                listProducts.Add(new Product
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    IsNew = product.IsNew,
                    ProductImage = product.ProductImage
                });
            }

            return listProducts;
        }

        public bool ExistColor(string namecolor, string codecolors, int id)
        {
            return _context.ProductColors.Any(c => c.ColorName == namecolor && c.ColorCode == codecolors && c.Id != id);
        }

        public async Task<Product> GetProductById(int productId)
        {
            return await _context.products.Include(x => x.Categories).Include(x => x.productColor).Where(x => x.Id == productId)
                   .SingleOrDefaultAsync();
        }
    }
}
        



