using System;
using System.ComponentModel.DataAnnotations;

namespace Task5.WebApi.ViewModels.Guest
{
    public class GuestBaseModel
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        [MaxLength(100)]
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
