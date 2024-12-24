using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Interfaces
{
    public interface IBrandRepository
    {
        Task<Brand> FindByIdAsync(int id);
    }
}
