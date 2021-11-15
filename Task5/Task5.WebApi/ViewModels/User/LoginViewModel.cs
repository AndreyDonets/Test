using System.ComponentModel.DataAnnotations;

namespace Task5.WebApi.ViewModels.User
{
    public class LoginViewModel : BaseUserViewModel
    {
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
    }
}
