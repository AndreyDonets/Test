using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task5.DAL.EF;
using Task5.DAL.Entities;
using Task5.DAL.Interfaces;

namespace Task5.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DataContext db;

        public UserRepository(DataContext db) => this.db = db;

        public void Create(User item) => db.Users.Add(item);
        public User Get(string id) => db.Users.Find(id);
        public User GetByEmail(string email) => db.Users.FirstOrDefault(x => x.Email == email);
        public IEnumerable<User> GetAll() => db.Users;
        public void Delete(string id)
        {
            var item = db.Users.Find(id);
            if (item != null)
                db.Users.Remove(item);
        }
        public void Update(User item) => db.Entry(item).State = EntityState.Modified;

        public async Task CreateAsync(User item)
        {
            db.Users.Add(item);
            await db.SaveChangesAsync();
        }
        public async Task<User> GetAsync(string id) => await db.Set<User>().FindAsync(id);
        public async Task<User> GetByEmailAsync(string email) => await db.Users.FirstOrDefaultAsync(x => x.Email == email);
        public async Task<IEnumerable<User>> GetAllAsync() => await db.Set<User>().ToListAsync();
        public async Task DeleteAsync(string id)
        {
            var item = db.Users.Find(id);
            if (item != null)
            {
                db.Set<User>().Remove(item);
                await db.SaveChangesAsync();
            }
        }
        public async Task UpdateAsync(User item)
        {
            db.Entry(item).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
