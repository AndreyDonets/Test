using System;

namespace Task5.BLL.DTO
{
    public class StayDTO : BaseModelDTO
    {
        public StayDTO() => Id = Guid.NewGuid();

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool CheckedIn { get; set; }
        public bool CheckedOut { get; set; }
        public Guid RoomId { get; set; }
        public Guid GuestId { get; set; }
    }
}