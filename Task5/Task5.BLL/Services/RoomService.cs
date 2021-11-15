using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task5.BLL.DTO;
using Task5.BLL.Interfaces;
using Task5.DAL.Entities;
using Task5.DAL.Interfaces;

namespace Task5.BLL.Services
{
    public class RoomService : IRoomService
    {
        private IUnitOfWork db;

        public RoomService(IUnitOfWork db) => this.db = db;

        private IMapper GetMapperToRoomDTO() => new MapperConfiguration(cfg => cfg.CreateMap<Room, RoomDTO>()).CreateMapper();
        private IMapper GetMapperToRoom() => new MapperConfiguration(cfg => cfg.CreateMap<RoomDTO, Room>()).CreateMapper();

        public IEnumerable<RoomDTO> GetAll()
        {
            var s = db.RoomRepository.GetAll().ToList();
            var result = GetMapperToRoomDTO().Map<IEnumerable<Room>, List<RoomDTO>>(db.RoomRepository.GetAll());
            return result;
        }
            public RoomDTO Get(Guid id) => GetMapperToRoomDTO().Map<Room, RoomDTO>(db.RoomRepository.Get(id));
        public void Create(RoomDTO item) => db.RoomRepository.Create(GetMapperToRoom().Map<RoomDTO, Room>(item));
        public void Update(RoomDTO item) => db.RoomRepository.Update(GetMapperToRoom().Map<RoomDTO, Room>(item));
        public void Delete(Guid id) => db.RoomRepository.Delete(id);
        public void Save() => db.Save();

        public async Task<IEnumerable<RoomDTO>> GetAllAsync() => GetMapperToRoomDTO().Map<IEnumerable<Room>, List<RoomDTO>>(await db.RoomRepository.GetAllAsync());
        public async Task<RoomDTO> GetAsync(Guid id) => GetMapperToRoomDTO().Map<Room, RoomDTO>(await db.RoomRepository.GetAsync(id));
        public async Task CreateAsync(RoomDTO item) => await db.RoomRepository.CreateAsync(GetMapperToRoom().Map<RoomDTO, Room>(item));
        public async Task UpdateAsync(RoomDTO item) => await db.RoomRepository.UpdateAsync(GetMapperToRoom().Map<RoomDTO, Room>(item));
        public async Task DeleteAsync(Guid id) => await db.RoomRepository.DeleteAsync(id);

        public void Dispose() => db.Dispose();
    }
}
