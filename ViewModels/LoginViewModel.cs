using System.ComponentModel.DataAnnotations;

namespace Coursework.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "UsernameRequired")]
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "PasswordRequired")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "RememberMe")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
