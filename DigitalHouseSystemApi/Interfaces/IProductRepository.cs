using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> FindByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        void Save(Product product);
        Task<bool> SaveAllAsync();
        void DeletePhoto(Photo photo);
    }
}
