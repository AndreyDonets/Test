using System.ComponentModel.DataAnnotations;

namespace Task5.WebApi.ViewModels.Room
{
    public class AddRoomViewModel : BaseRoomViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Category { get; set; }
    }
}
