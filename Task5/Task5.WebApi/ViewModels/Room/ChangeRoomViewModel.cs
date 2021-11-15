using System.ComponentModel.DataAnnotations;

namespace Task5.WebApi.ViewModels.Room
{
    public class ChangeRoomViewModel : BaseRoomViewModel
    {
        public int NewNumber { get; set; }
        [MaxLength(100)]
        public string NewCategory { get; set; }
    }
}
