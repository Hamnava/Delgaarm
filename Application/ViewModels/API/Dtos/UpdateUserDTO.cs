using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.API.Dtos
{
    public class UpdateUserDTO
    {
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Profile { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string PostalAddress { get; set; }
    }
}
