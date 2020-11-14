using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SpadManagement.Web.Models
{

    public class ExternalLoginConfirmationViewModel
    {
        [Required(ErrorMessage = "نام کاربری وارد نشده است")]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required(ErrorMessage = "گذرواژه وارد نشده است")]
        [DataType(DataType.Password)]
        [Display(Name = "گذرواژه")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "تکرار گذرواژه وارد نشده است")]
        [StringLength(100, ErrorMessage = "طول گذرواژه باید حداقل 2 کاراکتر باشد", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "گذرواژه جدید")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "بازیابی گذرواژه")]
        [Compare("NewPassword", ErrorMessage = "گذرواژه و تکرار رمز عبور مطابقت ندارند.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "نام کاربری وارد نشده است")]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "گذرواژه وارد نشده است")]
        [DataType(DataType.Password)]
        [Display(Name = "گذرواژه")]
        public string Password { get; set; }

        [Display(Name = "مرا به خاطر بسپار?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "نام کاربری وارد نشده است")]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "گذرواژه وارد نشده است")]
        [StringLength(100, ErrorMessage = "طول گذرواژه باید حداقل 2 کاراکتر باشد", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "گذرواژه")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "بازیابی گذرواژه")]
        [Compare("Password", ErrorMessage = "گذرواژه و تکرار رمز عبور مطابقت ندارند.")]
        public string ConfirmPassword { get; set; }
    }
}