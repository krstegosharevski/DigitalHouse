using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Services
{
    public interface IBrandService
    {
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
    }
}
