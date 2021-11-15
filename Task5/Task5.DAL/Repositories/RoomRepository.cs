using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task5.DAL.EF;
using Task5.DAL.Entities;
using Task5.DAL.Interfaces;

namespace Task5.DAL.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private DataContext db;

        public RoomRepository(DataContext db) => this.db = db;

        public void Create(Room item) => db.Rooms.Add(item);
        public Room Get(Guid id) => db.Rooms.Find(id);
        public IEnumerable<Room> GetAll() => db.Rooms;
        public void Delete(Guid id)
        {
            var item = db.Rooms.Find(id);
            if (item != null)
                db.Rooms.Remove(item);
        }
        public void Update(Room item) => db.Entry(item).State = EntityState.Modified;

        public async Task CreateAsync(Room item)
        {
            db.Rooms.Add(item);
            await db.SaveChangesAsync();
        }
        public async Task<Room> GetAsync(Guid id) => await db.Set<Room>().FindAsync(id);
        public async Task<IEnumerable<Room>> GetAllAsync() => await db.Set<Room>().ToListAsync();
        public async Task DeleteAsync(Guid id)
        {
            var item = db.Rooms.Find(id);
            if (item != null)
            {
                db.Set<Room>().Remove(item);
                await db.SaveChangesAsync();
            }
        }
        public async Task UpdateAsync(Room item)
        {
            db.Entry(item).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
