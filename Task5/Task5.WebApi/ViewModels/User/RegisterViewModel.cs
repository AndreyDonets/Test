using System.ComponentModel.DataAnnotations;

namespace Task5.WebApi.ViewModels.User
{
    public class RegisterViewModel : LoginViewModel
    {
        [Required]
        [MaxLength(50)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }
        [Required]
        [MaxLength(50)]
        public string Role { get; set; }
    }
}
