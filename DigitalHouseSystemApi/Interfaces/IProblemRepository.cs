using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Interfaces
{
    public interface IProblemRepository
    {
        Task<IEnumerable<Problem>> FindAllAsync();
    }
}
