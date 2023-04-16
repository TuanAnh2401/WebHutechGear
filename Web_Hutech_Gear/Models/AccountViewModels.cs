using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web_Hutech_Gear.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Xin vui lòng nhập tên của bạn")]
        [Display(Name = "FullName")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Xin vui lòng nhập địa chỉ của bạn")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Xin vui lòng nhập số điện thoại của bạn")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(0|84)([0-9]{9})$", ErrorMessage = "Xin vui lòng nhập đúng định dạng số điện thoại")]
        public string PhoneNumber { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage ="Vui lòng nhập tài khoản")]
        [Display(Name = "Account")]
        public string Account { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = " Nhớ tài khoản")]
        public bool RememberMe { get; set; }
    }
    public class UpdateAccountViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
    }
    public class CreateAccountViewModel
    {

        [Required(ErrorMessage = "Xin vui lòng nhập tên của bạn")]
        [Display(Name = "FullName")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Xin vui lòng nhập địa chỉ của bạn")]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Xin vui lòng nhập địa chỉ Email của bạn")]
        [EmailAddress(ErrorMessage = "Xin vui lòng nhập đúng định dạng địa chỉ email")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public string Role { get; set; }

        [Required(ErrorMessage = "Xin vui lòng nhập số điện thoại của bạn")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(0|84)([0-9]{9})$", ErrorMessage = "Xin vui lòng nhập đúng định dạng số điện thoại")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Xin vui lòng nhập tên tài khoản")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Xin vui lòng nhập mật khẩu")]
        [StringLength(100, ErrorMessage = "{0} phải dài ít nhất {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password, ErrorMessage = "Mật khẩu phải có kí tự chữ số, chữ hoa, chữ thường và 1 kí tự đặc biệt")]
        [Display(Name = "Mật Khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Mật khẩu và mật khẩu xác nhận không khớp.")]
        public string ConfirmPassword { get; set; }
    }
    public class UpDate
    {
        [Display(Name = "Avatar")]
        public string Avatar { get; set; }
        [Required(ErrorMessage = "Xin vui lòng nhập tên của bạn")]
        [Display(Name = "FullName")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Xin vui lòng nhập địa chỉ của bạn")]
        [Display(Name = "Address")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(0|84)([0-9]{9})$", ErrorMessage = "Xin vui lòng nhập đúng định dạng số điện thoại")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Xin vui lòng nhập địa chỉ của bạn")]
        [Display(Name = "Address")]
        public string Address { get; set; }
        public string Email { get; set; }

    }
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Xin vui lòng nhập tên của bạn")]
        [Display(Name = "FullName")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Xin vui lòng nhập địa chỉ của bạn")]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Xin vui lòng nhập địa chỉ Email của bạn")]
        [EmailAddress(ErrorMessage = "Xin vui lòng nhập đúng định dạng địa chỉ email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Xin vui lòng nhập số điện thoại của bạn")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(0|84)([0-9]{9})$", ErrorMessage = "Xin vui lòng nhập đúng định dạng số điện thoại")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Xin vui lòng nhập tên tài khoản")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Xin vui lòng nhập mật khẩu")]
        [StringLength(100, ErrorMessage = "{0} phải dài ít nhất {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password, ErrorMessage = "Mật khẩu phải có kí tự chữ số, chữ hoa, chữ thường và 1 kí tự đặc biệt")]
        [Display(Name = "Mật Khẩu")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Xin vui lòng nhập lại mật khẩu")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Mật khẩu và mật khẩu xác nhận không khớp.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Xin vui lòng nhập địa chỉ Email của bạn")]
        [EmailAddress(ErrorMessage = "Xin vui lòng nhập đúng định dạng địa chỉ email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Xin vui lòng nhập mật khẩu")]
        [StringLength(100, ErrorMessage = "{0} phải dài ít nhất {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password, ErrorMessage = "Mật khẩu phải có kí tự chữ số, chữ hoa, chữ thường và 1 kí tự đặc biệt")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Xin vui lòng nhập lại mật khẩu")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Mật khẩu và mật khẩu xác nhận không khớp.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Xin vui lòng nhập địa chỉ Email của bạn")]
        [EmailAddress(ErrorMessage = "Xin vui lòng nhập đúng định dạng địa chỉ email")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
