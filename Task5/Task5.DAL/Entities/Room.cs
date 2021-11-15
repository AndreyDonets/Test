using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task5.DAL.Entities
{
    public class Room : BaseEntity
    {
        public Room() => Id = Guid.NewGuid();

        public int Number { get; set; }
        public Guid CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        public virtual IEnumerable<Stay> Stays { get; set; }
    }
}
