using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class EditColorViewModel
    {
        public int Id { get; set; }

        public string ColorName { get; set; }

        public string ColorCode { get; set; }

        public bool IsDelete { get; set; }
    }
}
