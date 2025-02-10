using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Interfaces
{
    public interface IBrandRepository
    {
        Task<Brand> FindByIdAsync(int id);
        Task<IEnumerable<Brand>> GetAllBrandsAsync();
        Task<IEnumerable<Brand>> GetAllBrandsByProductCategoryAsync(int categoryId);
    }
}
