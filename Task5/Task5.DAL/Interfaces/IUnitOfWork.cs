using System;

namespace Task5.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGuestRepository GuestRepository { get; }
        IUserRepository UserRepository { get; }
        IRoomRepository RoomRepository { get; }
        IStayRepository StayRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        ICategoryDateRepository CategoryDateRepository { get; }
        void Save();
    }
}
