using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> FindByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<Product>> GetAllProductsByCategoryIdAsync(int id);
        void Save(Product product);
        Task<bool> SaveAllAsync();
        void DeletePhoto(Photo photo);
    }
}
