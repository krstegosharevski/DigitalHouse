using DigitalHouseSystemApi.Interfaces;
using DigitalHouseSystemApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalHouseSystemApi.Data
{
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context; 
        }

        public async Task<AppUser> GetUserByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
