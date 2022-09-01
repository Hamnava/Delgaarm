using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Entities
{
    public class Product
    {
        [Key]
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

        public DateTime ProductCreate { get; set; } = DateTime.UtcNow;
        public float MainPrice { get; set; }
        public int? sepcialprice { get; set; }
        public bool IsNew { get; set; } = true;
        public bool IsActive { get; set; } = true;
        public bool IsDelete { get; set; } = false;
        public int CategoryId { get; set; }
        public int ColorId { get; set; }
        public int count { get; set; }

        [ForeignKey("CategoryId")]
        public Category Categories { get; set; }

        [ForeignKey("ColorId")]
        public ProductColor productColor { get; set; }

        public ICollection<ProductGallery> ProductGalleries { get; set; }

        public List<Order> orders { get; set; }

    }
}
