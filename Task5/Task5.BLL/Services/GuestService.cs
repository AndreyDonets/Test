using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task5.BLL.DTO;
using Task5.BLL.Interfaces;
using Task5.DAL.Entities;
using Task5.DAL.Interfaces;

namespace Task5.BLL.Services
{
    public class GuestService : IGuestService
    {
        private IUnitOfWork db;

        public GuestService(IUnitOfWork db) => this.db = db;

        private IMapper GetMapperToGuestDTO() => new MapperConfiguration(cfg => cfg.CreateMap<Guest, GuestDTO>()).CreateMapper();
        private IMapper GetMapperToGuest() => new MapperConfiguration(cfg => cfg.CreateMap<GuestDTO, Guest>()).CreateMapper();

        public IEnumerable<GuestDTO> GetAll() => GetMapperToGuestDTO().Map<IEnumerable<Guest>, List<GuestDTO>>(db.GuestRepository.GetAll());
        public GuestDTO Get(Guid id) => GetMapperToGuestDTO().Map<Guest, GuestDTO>(db.GuestRepository.Get(id));
        public void Create(GuestDTO item) => db.GuestRepository.Create(GetMapperToGuest().Map<GuestDTO, Guest>(item));
        public void Update(GuestDTO item) => db.GuestRepository.Update(GetMapperToGuest().Map<GuestDTO, Guest>(item));
        public void Delete(Guid id) => db.GuestRepository.Delete(id);
        public void Save() => db.Save();

        public async Task<IEnumerable<GuestDTO>> GetAllAsync() => GetMapperToGuestDTO().Map<IEnumerable<Guest>, List<GuestDTO>>(await db.GuestRepository.GetAllAsync());
        public async Task<GuestDTO> GetAsync(Guid id) => GetMapperToGuestDTO().Map<Guest, GuestDTO>(await db.GuestRepository.GetAsync(id));
        public async Task CreateAsync(GuestDTO item) => await db.GuestRepository.CreateAsync(GetMapperToGuest().Map<GuestDTO, Guest>(item));
        public async Task UpdateAsync(GuestDTO item) => await db.GuestRepository.UpdateAsync(GetMapperToGuest().Map<GuestDTO, Guest>(item));
        public async Task DeleteAsync(Guid id) => await db.GuestRepository.DeleteAsync(id);

        public void Dispose() => db.Dispose();
    }
}
