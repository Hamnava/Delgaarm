using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Entities
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }

        public ApplicationUser User { get; set; }
        public int UserId { get; set; }
    }
}
