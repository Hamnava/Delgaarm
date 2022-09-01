using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multipisus.Core.ViewModels.API.Dtos
{
    public class AboutDTo
    {
        public string Title { get; set; }
        public string Descriptions { get; set; }
        public string VideoLink { get; set; }

        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }


    public class QuestionsDTO
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
