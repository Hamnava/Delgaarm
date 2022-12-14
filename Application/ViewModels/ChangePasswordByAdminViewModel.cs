using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class ChangePasswordByAdminViewModel
    {
        public string Id { get; set; }

        [Display(Name = "رمز عبور جدید")]
        public string NewPassword { get; set; }

        [Display(Name = "تکرار رمز عبور")]
        [Compare("NewPassword", ErrorMessage = "Your confirm passowrd doesn't match")]
        public string ConfirmNewPassword { get; set; }
    }
}
