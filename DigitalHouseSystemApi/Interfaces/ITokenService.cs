using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}
