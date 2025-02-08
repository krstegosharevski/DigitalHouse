using System.Globalization;
using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Helpers;
using DigitalHouseSystemApi.Interfaces;
using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Services
{
    public interface IProductService
    {
        Task<Product> FindByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<PagedList<ProductDto>> GetAllProductsByCategoryAsync(string category, ProductParams productParams);
        Task<ProductDto> AddProductAsync(AddProductDto productDto ,IFormFile file);
        Task<ProductDto> EditProductAsync(int id,AddProductDto productDto, IFormFile file);
        Task<bool> AddPhotoAsync(IFormFile file, int productId);
        Task DeletePhoto(int id);
        Task<ICollection<SearchProductDto>> GetAllSearchProductsAsync(string search);
        Task<ProductDto> GetSearchedProductAsync(string name);
        Task<GetProductEditDto> GetProductEditAsync(int id);
    }
}
