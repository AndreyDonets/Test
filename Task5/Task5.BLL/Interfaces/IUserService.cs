using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task5.BLL.DTO;

namespace Task5.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        UserDTO GetByEmail(string email);
        IEnumerable<UserDTO> GetAll();
        void Save();

        Task<UserDTO> GetByEmailAsync(string email);
        Task<IEnumerable<UserDTO>> GetAllAsync();
    }
}
