using DigitalHouseSystemApi.Data.Mappers;
using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Interfaces;
using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Services.Impl
{
    public class CategoryService : ICategoryService
    {
        public readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryDto>> FindAllCategoriesAsync()
        {
            var categories =  await _categoryRepository.GetAllAsync();
            List<CategoryDto> categoryDtos = new List<CategoryDto>();

            foreach(var category in categories)
            {
                categoryDtos.Add(category.MappToDtoModel());
            }
            return categoryDtos;
        }

        public async Task<Category> FindByIdAsync(int id)
        {
            return await _categoryRepository.FindByIdAsync(id);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _categoryRepository.SaveAllAsync();
        }
    }
}
