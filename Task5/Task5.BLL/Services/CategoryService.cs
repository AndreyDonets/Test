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
    public class CategoryService : ICategoryService
    {
        private IUnitOfWork db;

        public CategoryService(IUnitOfWork db) => this.db = db;

        private IMapper GetMapperToCategoryDTO() => new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryDTO>()).CreateMapper();
        private IMapper GetMapperToCategory() => new MapperConfiguration(cfg => cfg.CreateMap<CategoryDTO, Category>()).CreateMapper();

        public IEnumerable<CategoryDTO> GetAll() => GetMapperToCategoryDTO().Map<IEnumerable<Category>, List<CategoryDTO>>(db.CategoryRepository.GetAll());
        public CategoryDTO Get(Guid id) => GetMapperToCategoryDTO().Map<Category, CategoryDTO>(db.CategoryRepository.Get(id));
        public void Create(CategoryDTO item) => db.CategoryRepository.Create(GetMapperToCategory().Map<CategoryDTO, Category>(item));
        public void Update(CategoryDTO item) => db.CategoryRepository.Update(GetMapperToCategory().Map<CategoryDTO, Category>(item));
        public void Delete(Guid id) => db.CategoryRepository.Delete(id);
        public void Save() => db.Save();

        public async Task<IEnumerable<CategoryDTO>> GetAllAsync() => GetMapperToCategoryDTO().Map<IEnumerable<Category>, List<CategoryDTO>>(await db.CategoryRepository.GetAllAsync());
        public async Task<CategoryDTO> GetAsync(Guid id) => GetMapperToCategoryDTO().Map<Category, CategoryDTO>(await db.CategoryRepository.GetAsync(id));
        public async Task CreateAsync(CategoryDTO item) => await db.CategoryRepository.CreateAsync(GetMapperToCategory().Map<CategoryDTO, Category>(item));
        public async Task UpdateAsync(CategoryDTO item) => await db.CategoryRepository.UpdateAsync(GetMapperToCategory().Map<CategoryDTO, Category>(item));
        public async Task DeleteAsync(Guid id) => await db.CategoryRepository.DeleteAsync(id);

        public void Dispose() => db.Dispose();
    }
}
