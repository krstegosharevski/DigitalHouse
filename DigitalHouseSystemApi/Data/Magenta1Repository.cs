using DigitalHouseSystemApi.Interfaces;
using DigitalHouseSystemApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalHouseSystemApi.Data
{
    public class Magenta1Repository : IMagenta1Repository
    {
        private readonly ApplicationDbContext _context;

        public Magenta1Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Magenta1>> GetAllAsync()
        {
            return await _context.Magenta1s
                .Include(p => p.InternetPackage)
                .Include (p => p.AppUser)
                .Include(p => p.Magenta1Tariffs)
                .ThenInclude(p => p.Tariff)
                .ToListAsync();
        }
    }
}
