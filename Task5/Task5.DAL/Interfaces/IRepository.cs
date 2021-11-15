using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Task5.DAL.Interfaces
{
    public interface IRepository<T> 
        where T: class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(Guid id);
        Task CreateAsync(T item);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(T item);

        IEnumerable<T> GetAll();
        T Get(Guid id);
        void Create(T item);
        void Delete(Guid id);
        void Update(T item);
    }
}
