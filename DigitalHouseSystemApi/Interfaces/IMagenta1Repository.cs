using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Interfaces
{
    public interface IMagenta1Repository
    {
        Task<IEnumerable<Magenta1>> GetAllAsync();
    }
}
