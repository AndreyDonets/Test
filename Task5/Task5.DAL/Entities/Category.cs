using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Task5.DAL.Entities
{
    public class Category : BaseEntity
    {
        public Category() => Id = Guid.NewGuid();

        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual IEnumerable<Room> Rooms { get; set; }
        public virtual IEnumerable<CategoryDate> CategoryDates { get; set; }
    }
}