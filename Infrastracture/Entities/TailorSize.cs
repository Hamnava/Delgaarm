using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Entities
{
    public class TailorSize
    {
        [Key]
        public int Id { get; set; }
        public float Shane { get; set; }
        public float Astin { get; set; }
        public float Kamar { get; set; }
        public float ZerBaghal { get; set; }
        public float Shelwar { get; set; }
        public float perahan { get; set; }
        public string Detail { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}
