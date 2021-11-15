using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task5.DAL.EF;
using Task5.DAL.Entities;
using Task5.DAL.Interfaces;

namespace Task5.DAL.Repositories
{
    public class StayRepository : IStayRepository
    {
        private DataContext db;

        public StayRepository(DataContext db) => this.db = db;

        public void Create(Stay item) => db.Stays.Add(item);
        public Stay Get(Guid id) => db.Stays.Find(id);
        public IEnumerable<Stay> GetAll() => db.Stays;
        public void Delete(Guid id)
        {
            var item = db.Stays.Find(id);
            if (item != null)
                db.Stays.Remove(item);
        }
        public void Update(Stay item) => db.Entry(item).State = EntityState.Modified;

        public async Task CreateAsync(Stay item)
        {
            db.Stays.Add(item);
            await db.SaveChangesAsync();
        }
        public async Task<Stay> GetAsync(Guid id) => await db.Set<Stay>().FindAsync(id);
        public async Task<IEnumerable<Stay>> GetAllAsync() => await db.Set<Stay>().ToListAsync();
        public async Task DeleteAsync(Guid id)
        {
            var item = db.Stays.Find(id);
            if (item != null)
            {
                db.Set<Stay>().Remove(item);
                await db.SaveChangesAsync();
            }
        }
        public async Task UpdateAsync(Stay item)
        {
            db.Entry(item).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
