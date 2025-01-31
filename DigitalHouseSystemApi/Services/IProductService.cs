using System.Globalization;
using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Interfaces;
using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Services
{
    public interface IProductService
    {
        Task<Product> FindByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<ProductDto>> GetAllProductsByCategoryAsync(string category);
        Task<ProductDto> AddProductAsync(AddProductDto productDto ,IFormFile file);
        Task<bool> AddPhotoAsync(IFormFile file, int productId);
        Task DeletePhoto(int id);
        Task<ICollection<SearchProductDto>> GetAllSearchProductsAsync(string search);
        Task<ProductDto> GetSearchedProductAsync(string name);
    }
}
