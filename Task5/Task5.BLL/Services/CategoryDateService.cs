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
    public class CategoryDateService : ICategoryDateService
    {
        private IUnitOfWork db;

        public CategoryDateService(IUnitOfWork db) => this.db = db;

        private IMapper GetMapperToCategoryDateDTO() => new MapperConfiguration(cfg => cfg.CreateMap<CategoryDate, CategoryDateDTO>()).CreateMapper();
        private IMapper GetMapperToCategoryDate() => new MapperConfiguration(cfg => cfg.CreateMap<CategoryDateDTO, CategoryDate>()).CreateMapper();

        public IEnumerable<CategoryDateDTO> GetAll() => GetMapperToCategoryDateDTO().Map<IEnumerable<CategoryDate>, List<CategoryDateDTO>>(db.CategoryDateRepository.GetAll());
        public CategoryDateDTO Get(Guid id) => GetMapperToCategoryDateDTO().Map<CategoryDate, CategoryDateDTO>(db.CategoryDateRepository.Get(id));
        public void Create(CategoryDateDTO item) => db.CategoryDateRepository.Create(GetMapperToCategoryDate().Map<CategoryDateDTO, CategoryDate>(item));
        public void Update(CategoryDateDTO item) => db.CategoryDateRepository.Update(GetMapperToCategoryDate().Map<CategoryDateDTO, CategoryDate>(item));
        public void Delete(Guid id) => db.CategoryDateRepository.Delete(id);
        public void Save() => db.Save();

        public async Task<IEnumerable<CategoryDateDTO>> GetAllAsync() => GetMapperToCategoryDateDTO().Map<IEnumerable<CategoryDate>, List<CategoryDateDTO>>(await db.CategoryDateRepository.GetAllAsync());
        public async Task<CategoryDateDTO> GetAsync(Guid id) => GetMapperToCategoryDateDTO().Map<CategoryDate, CategoryDateDTO>(await db.CategoryDateRepository.GetAsync(id));
        public async Task CreateAsync(CategoryDateDTO item) => await db.CategoryDateRepository.CreateAsync(GetMapperToCategoryDate().Map<CategoryDateDTO, CategoryDate>(item));
        public async Task UpdateAsync(CategoryDateDTO item) => await db.CategoryDateRepository.UpdateAsync(GetMapperToCategoryDate().Map<CategoryDateDTO, CategoryDate>(item));
        public async Task DeleteAsync(Guid id) => await db.CategoryDateRepository.DeleteAsync(id);

        public void Dispose() => db.Dispose();
    }
}
