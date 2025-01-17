using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> FindByIdAsync(int id);
        Task<Category> FindByNameAsync(string name);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<bool> SaveAllAsync();
    }
}
