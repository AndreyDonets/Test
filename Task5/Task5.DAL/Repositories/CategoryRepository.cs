using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task5.DAL.EF;
using Task5.DAL.Entities;
using Task5.DAL.Interfaces;

namespace Task5.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private DataContext db;

        public CategoryRepository(DataContext db) => this.db = db;

        public void Create(Category item) => db.Categories.Add(item);
        public Category Get(Guid id) => db.Categories.Find(id);
        public IEnumerable<Category> GetAll() => db.Categories;
        public void Delete(Guid id)
        {
            var item = db.Categories.Find(id);
            if (item != null)
                db.Categories.Remove(item);
        }
        public void Update(Category item) => db.Entry(item).State = EntityState.Modified;

        public async Task CreateAsync(Category item)
        {
            db.Categories.Add(item);
            await db.SaveChangesAsync();
        }
        public async Task<Category> GetAsync(Guid id) => await db.Set<Category>().FindAsync(id);
        public async Task<IEnumerable<Category>> GetAllAsync() => await db.Set<Category>().ToListAsync();
        public async Task DeleteAsync(Guid id)
        {
            var item = db.Categories.Find(id);
            if (item != null)
            {
                db.Set<Category>().Remove(item);
                await db.SaveChangesAsync();
            }
        }
        public async Task UpdateAsync(Category item)
        {
            db.Entry(item).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
