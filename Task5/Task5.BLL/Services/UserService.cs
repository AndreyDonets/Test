using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task5.BLL.DTO;
using Task5.BLL.Interfaces;
using Task5.DAL.Entities;
using Task5.DAL.Interfaces;

namespace Task5.BLL.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork db;

        public UserService(IUnitOfWork db) => this.db = db;

        private IMapper GetMapperToUserDTO() => new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
        private IMapper GetMapperToUser() => new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, User>()).CreateMapper();


        public UserDTO GetByEmail(string email) => GetMapperToUserDTO().Map<User, UserDTO>(db.UserRepository.GetByEmail(email));
        public IEnumerable<UserDTO> GetAll() => GetMapperToUserDTO().Map<IEnumerable<User>, List<UserDTO>>(db.UserRepository.GetAll());
        public void Save() => db.Save();

        public async Task<UserDTO> GetByEmailAsync(string email) => GetMapperToUserDTO().Map<User, UserDTO>(await db.UserRepository.GetByEmailAsync(email));
        public async Task<IEnumerable<UserDTO>> GetAllAsync() => GetMapperToUserDTO().Map<IEnumerable<User>, List<UserDTO>>(await db.UserRepository.GetAllAsync());

        public void Dispose() => db.Dispose();
        
    }
}
