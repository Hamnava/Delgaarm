using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class EditUserViewModel
    {
        [Display(Name = "جنس")]
        [Required(ErrorMessage = "این فیلد نمیتواند خالی باشد")]
        public string Gender { get; set; }
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "این فیلد نمیتواند خالی باشد")]
        public string Id { get; set; }
        public string UserName { get; set; }
        [Display(Name = "نام کامل")]
        [Required(ErrorMessage = "این فیلد نمیتواند خالی باشد")]
        public string FullName { get; set; }
        [Display(Name = "شماره تماس")]
        [Required(ErrorMessage = "این فیلد نمیتواند خالی باشد")]
        public string PhoneNumber { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "این فیلد نمیتواند خالی باشد")]
        public string Email { get; set; }

        public string EmailPassword { get; set; }
        public string Profile { get; set; }

        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public bool IsAdmin { get; set; }
        [Display(Name = "کشور")]
        [Required(ErrorMessage = "این فیلد نمیتواند خالی باشد")]
        public string Country { get; set; }
        [Display(Name = "شهر")]
        [Required(ErrorMessage = "این فیلد نمیتواند خالی باشد")]
        public string City { get; set; }
        [Display(Name = "آدرس پستی")]
        [Required(ErrorMessage = "این فیلد نمیتواند خالی باشد")]
        public string PostalAddress { get; set; }
    }
}
