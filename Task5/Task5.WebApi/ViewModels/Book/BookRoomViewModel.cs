using System;
using System.ComponentModel.DataAnnotations;
using Task5.WebApi.ViewModels.Room;

namespace Task5.WebApi.ViewModels.Book
{
    public class BookRoomViewModel : BaseRoomViewModel
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        [MaxLength(100)]
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
        [Required]
        [MaxLength(50)]
        public string Passport { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
