using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> FindByIdAsync(int id);
        Task<bool> SaveAllAsync();
    }
}
