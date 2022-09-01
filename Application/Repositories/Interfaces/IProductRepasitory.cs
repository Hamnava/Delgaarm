

using Infrastracture.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository.Interfaces
{
    public interface IProductRepasitory
    {
        Task<bool> GetProductByProductNameAsync(string productName, int id);
        Task<ProductGallery> GetGalleryByProductId(int productId);

        List<Product> Search(string text, List<int> categoryid, int sort = 1);

        bool ExistColor(string namecolor, string codecolors,int id);

        Task<Product> GetProductById(int productId);

    }
}
