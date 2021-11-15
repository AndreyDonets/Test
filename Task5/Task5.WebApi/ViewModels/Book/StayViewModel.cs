using System;

namespace Task5.WebApi.ViewModels.Book
{
    public class StayViewModel
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool CheckedIn { get; set; }
        public bool CheckedOut { get; set; }
        public Guid RoomId { get; set; }
        public Guid GuestId { get; set; }
    }
}
