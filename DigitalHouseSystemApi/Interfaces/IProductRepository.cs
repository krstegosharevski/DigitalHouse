using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Helpers;
using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> FindByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<PagedList<Product>> GetAllProductsByCategoryIdAsync(int id, ProductParams productParams);
        void Save(Product product);
        Task<bool> SaveAllAsync();
        void DeletePhoto(Photo photo);
        Task<Product> FindProductByName(string name);
        Task<IEnumerable<Product>> SearchByNameProductsAsync(string search);
    }
}
