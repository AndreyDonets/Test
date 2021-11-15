using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Task5.DAL.Entities;

namespace Task5.DAL.EF
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) =>
            Database.EnsureCreated();

        public DbSet<Guest> Guests { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryDate> CategoryDates { get; set; }
        public DbSet<Stay> Stays { get; set; }
    }
}
