using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Task5.BLL.Interfaces
{
    public interface IBaseService<T> : IDisposable
        where T: class
    {
        T Get(Guid id);
        IEnumerable<T> GetAll();
        void Create(T item);
        void Update(T item);
        void Delete(Guid id);
        void Save();

        Task<T> GetAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task CreateAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(Guid id);
    }
}