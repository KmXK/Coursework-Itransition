using System.ComponentModel.DataAnnotations;

namespace Coursework.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "UsernameRequired")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "PasswordRequired")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword")]
        [Compare("Password", ErrorMessage = "ConfirmPasswordNotEqual")]
        public string ConfirmPassword { get; set; }
    }
}
