using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class ServiceViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descriptions { get; set; }
        public string? ImageUrl { get; set; }
        public string Alt { get; set; }
        public string Title { get; set; }
        public bool IsDelete { get; set; }
    }
}
