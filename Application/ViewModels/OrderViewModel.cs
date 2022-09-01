using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public double TotalAmount { get; set; }

        public int TotalItem { get; set; }


        public DateTime OrderTime { get; set; } = DateTime.UtcNow;


        public string OrderStatus { get; set; } = "در حال بررسی";


        public string PaymentStatus { get; set; }

        public string UserId { get; set; }
        public int ProductId { get; set; }
    }
}
