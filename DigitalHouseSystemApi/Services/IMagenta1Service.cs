using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Services
{
    public interface IMagenta1Service
    {
        Task<IEnumerable<Magenta1Dto>> GetAllMagenta1();
        Task<Magenta1Dto> UpdateStatusOnMagenta1(int id);
    }
}
