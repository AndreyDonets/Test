using System.Collections.Generic;
using System.Threading.Tasks;
using Task5.DAL.Entities;

namespace Task5.DAL.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetAsync(string id);
        Task<User> GetByEmailAsync(string email);
        Task CreateAsync(User item);
        Task DeleteAsync(string id);
        Task UpdateAsync(User item);

        IEnumerable<User> GetAll();
        User Get(string id);
        User GetByEmail(string email);
        void Create(User item);
        void Delete(string id);
        void Update(User item);
    }
}
