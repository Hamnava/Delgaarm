using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Entities
{
    public class ProductColor
    {
        [Key]
        public int Id { get; set; }

        public string ColorName { get; set; }

        public string ColorCode { get; set; }

        public bool IsDelete { get; set; }

        public List<Product> products { get; set; }

    }
}
