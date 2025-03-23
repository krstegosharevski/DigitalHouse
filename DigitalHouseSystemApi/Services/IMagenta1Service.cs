using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Services
{
    public interface IMagenta1Service
    {
        Task<IEnumerable<Magenta1Dto>> GetAllMagenta1();
    }
}
