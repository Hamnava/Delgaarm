using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public bool IsBestSeller { get; set; }
        public bool IsPrefered { get; set; }
        public byte producStars { get; set; }
        public string TekaType { get; set; }
        public string? ProductImage { get; set; }
        public string Alt { get; set; }
        public string Title { get; set; }
        public string ProductCode { get; set; }
        public string Slag { get; set; }
        public string Description { get; set; }

        public DateTime ProductCreate { get; set; }
        public float MainPrice { get; set; }
        public int? sepcialprice { get; set; }
        public bool IsNew { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public int CategoryId { get; set; }
        public int ColorId { get; set; }
        public int count { get; set; }
    }
}
