using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.API.Dtos
{
    public class ContactDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }

    public class SubscribeDto
    {
        public string Email { get; set; }
    }

    public class EmailSenderViewModel
    {
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
