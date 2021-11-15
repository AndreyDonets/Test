using Task5.DAL.EF;
using Task5.DAL.Interfaces;

namespace Task5.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext db;
        private bool disposed = false;

        public UnitOfWork(DataContext db,
                          IGuestRepository guestRepository,
                          IUserRepository userRepository,
                          IRoomRepository roomRepository,
                          IStayRepository stayRepository,
                          ICategoryRepository categoryRepository,
                          ICategoryDateRepository categoryDateRepository)
        {
            this.db = db;
            GuestRepository = guestRepository;
            UserRepository = userRepository;
            RoomRepository = roomRepository;
            StayRepository = stayRepository;
            CategoryRepository = categoryRepository;
            CategoryDateRepository = categoryDateRepository;
        }

        public IGuestRepository GuestRepository { get; }

        public IUserRepository UserRepository { get; }

        public IRoomRepository RoomRepository { get; }

        public IStayRepository StayRepository { get; }

        public ICategoryRepository CategoryRepository { get; }

        public ICategoryDateRepository CategoryDateRepository { get; }

        public void Dispose()
        {
            if (disposed)
            {
                db.Dispose();
                disposed = true;
            }
        }

        public void Save() => db.SaveChanges();
    }
}
