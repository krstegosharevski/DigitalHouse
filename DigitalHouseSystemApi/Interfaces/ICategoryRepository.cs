using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> FindByIdAsync(int id);
    }
}
