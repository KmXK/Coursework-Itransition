using System.ComponentModel.DataAnnotations;

namespace Coursework.ViewModels
{
    public class RegisterExternalViewModel
    {
        [Required(ErrorMessage = "UsernameRequired")]
        [Display(Name = "Username")]
        public string Username { get; set; }
    }
}
