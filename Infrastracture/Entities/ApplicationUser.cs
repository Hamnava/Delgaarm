using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? EmailPassword { get; set; }
        public string? ActiveCode { get; set; }
        public bool IsDelete { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.UtcNow;
        public List<Order> orders { get; set; }

    }
}
