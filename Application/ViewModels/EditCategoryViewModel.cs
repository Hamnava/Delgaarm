using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class EditCategoryViewModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string? CategoryPhoto { get; set; }
        public string Alt { get; set; }
        public string Title { get; set; }
        public bool IsDelete { get; set; }
    }
}
