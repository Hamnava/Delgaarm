using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class SliderViewModel
    {
        public int SliderId { get; set; }

        public string Caption { get; set; }

        public double price { get; set; }

        public string silderName { get; set; }

        public int sliderOrder { get; set; }

        public string? sliderImage { get; set; }
        public string Alt { get; set; }
        public string Title { get; set; }

        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
