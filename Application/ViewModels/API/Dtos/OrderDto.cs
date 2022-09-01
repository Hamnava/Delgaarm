using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.API.Dtos
{
    public class OrderDto
    {
        public double TotalAmount { get; set; }

        public int TotalItem { get; set; }

        public byte PaymentStatus { get; set; }

        public string UserId { get; set; }


        public int ProductId { get; set; }
    }
}
