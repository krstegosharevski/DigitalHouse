using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByUsername(string username);
    }
}
