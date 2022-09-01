using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{

    #region UserViewModels for API project
    public class UserDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

    }

    public class UserRegisterDTO
    {
        [Required(ErrorMessage = "Email feild should not be empty!")]
        [EmailAddress(ErrorMessage = "Please enter a correct email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password field should not be empty")]
        public string Password { get; set; }

    }

    public class LoginDTO
    {
        [Required(ErrorMessage = "Username feild should not be empty!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password field should not be empty")]
        public string Password { get; set; }
    }
    #endregion
    public class UserViewModel
    {
        [Display(Name = "جنس")]
        [Required(ErrorMessage ="این فیلد نمیتواند خالی باشد")]
        public string Gender { get; set; }
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "این فیلد نمیتواند خالی باشد")]
        //public string Id { get; set; }
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

    public class UserFullNameViewModel
    {
        public string UserId { get; set; }
        public string UserFullName { get; set; }
    }
}
