using System.ComponentModel.DataAnnotations;

namespace Task5.WebApi.ViewModels.User
{
    public class ChangeRoleViewModel : BaseUserViewModel
    {
        [Required]
        [MaxLength(20)]
        public string Role { get; set; }
    }
}
