using System.ComponentModel.DataAnnotations;

namespace Task5.WebApi.ViewModels.User
{
    public class BaseUserViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }
    }
}
