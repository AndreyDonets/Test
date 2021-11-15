using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Task5.DAL.Entities
{
    public class Guest : BaseEntity
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
       
        public virtual IEnumerable<Stay> Stays { get; set; }
    }
}
