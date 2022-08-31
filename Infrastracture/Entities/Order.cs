using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public double TotalAmount { get; set; }

        public int TotalItem { get; set; }


        public DateTime OrderTime { get; set; } = DateTime.UtcNow;


        public string OrderStatus { get; set; } = "Pending";


        public string PaymentStatus { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
