using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.API.Dtos
{
    public class ProductlistDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public bool IsBestSeller { get; set; }
        public bool IsPrefered { get; set; }
        public byte producStars { get; set; }
        public string TekaType { get; set; }
        public string ProductImage { get; set; }
        public string Alt { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime ProductCreate { get; set; }
        public float MainPrice { get; set; }
        public int sepcialprice { get; set; }
        public bool IsNew { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public int count { get; set; }
        public string CategoryName { get; set; }
        public string ColorName { get; set; }
        public string ColorCode { get; set; }
    }

   public class ProductGalleryDTO
    {
        public string ImageUrl { get; set; }
    }

    public class ProductParamForSearch
    {
        public int CategoryId { get; set; }
        public string productName { get; set; }
    }
}
