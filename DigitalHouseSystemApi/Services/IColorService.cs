using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Services
{
    public interface IColorService
    {
        Task<IEnumerable<ColorDto>> GetColorsAsync();
    }
}
