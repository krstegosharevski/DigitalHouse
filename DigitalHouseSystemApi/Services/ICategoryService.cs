using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Services
{
    public interface ICategoryService
    {
        Task<Category> FindByIdAsync(int id);
        Task<IEnumerable<CategoryDto>> FindAllCategoriesAsync();
        Task<bool> SaveAllAsync();

    }
}
