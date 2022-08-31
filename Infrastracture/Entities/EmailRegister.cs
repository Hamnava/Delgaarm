using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Entities
{
    public class EmailRegister
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime DateRegister { get; set; } = DateTime.UtcNow;
    }
}
