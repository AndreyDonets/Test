using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task5.DAL.Entities
{
    public class Stay : BaseEntity
    {
        public Stay() => Id = Guid.NewGuid();

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool CheckedIn { get; set; }
        public bool CheckedOut { get; set; }
        public Guid RoomId { get; set; }
        public Guid GuestId { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }
        [ForeignKey("GuestId")]
        public virtual Guest Guest { get; set; }
    }
}