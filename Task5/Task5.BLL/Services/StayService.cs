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
    public class StayService : IStayService
    {
        private IUnitOfWork db;

        public StayService(IUnitOfWork db) => this.db = db;

        private IMapper GetMapperToStayDTO() => new MapperConfiguration(cfg => cfg.CreateMap<Stay, StayDTO>()).CreateMapper();
        private IMapper GetMapperToStay() => new MapperConfiguration(cfg => cfg.CreateMap<StayDTO, Stay>()).CreateMapper();

        public IEnumerable<StayDTO> GetAll() => GetMapperToStayDTO().Map<IEnumerable<Stay>, List<StayDTO>>(db.StayRepository.GetAll());
        public StayDTO Get(Guid id) => GetMapperToStayDTO().Map<Stay, StayDTO>(db.StayRepository.Get(id));
        public void Create(StayDTO item) => db.StayRepository.Create(GetMapperToStay().Map<StayDTO, Stay>(item));
        public void Update(StayDTO item) => db.StayRepository.Update(GetMapperToStay().Map<StayDTO, Stay>(item));
        public void Delete(Guid id) => db.StayRepository.Delete(id);
        public void Save() => db.Save();

        public async Task<IEnumerable<StayDTO>> GetAllAsync() => GetMapperToStayDTO().Map<IEnumerable<Stay>, List<StayDTO>>(await db.StayRepository.GetAllAsync());
        public async Task<StayDTO> GetAsync(Guid id) => GetMapperToStayDTO().Map<Stay, StayDTO>(await db.StayRepository.GetAsync(id));
        public async Task CreateAsync(StayDTO item) => await db.StayRepository.CreateAsync(GetMapperToStay().Map<StayDTO, Stay>(item));
        public async Task UpdateAsync(StayDTO item) => await db.StayRepository.UpdateAsync(GetMapperToStay().Map<StayDTO, Stay>(item));
        public async Task DeleteAsync(Guid id) => await db.StayRepository.DeleteAsync(id);

        public void Dispose() => db.Dispose();
    }
}
