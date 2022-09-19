using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class YourDesignViewModel
    {
        public int Id { get; set; }
        public string Images { get; set; }
        public string Detail { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsConfirmed { get; set; }
        public string UserId { get; set; }
    }
}
