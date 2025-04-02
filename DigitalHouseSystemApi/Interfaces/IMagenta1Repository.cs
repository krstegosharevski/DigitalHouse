using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Interfaces
{
    public interface IMagenta1Repository
    {
        Task<IEnumerable<Magenta1>> GetAllAsync();
        Task<Magenta1?> FindById(int id);
        Task<bool> ApproveMagenta1User(Magenta1 magenta1);
        Task AddAsync(Magenta1 entity);
        Task SaveChangesAsync();
    }
}
