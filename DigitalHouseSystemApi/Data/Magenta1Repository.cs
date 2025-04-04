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
                .Where(p => p.Approved == false)
                .ToListAsync();
        }

        public async Task<bool> ApproveMagenta1User(Magenta1 magenta1)
        {
            if (magenta1 == null)
            {
                return false; 
            }

            magenta1.Approved = true; 
            await _context.SaveChangesAsync();

            return true; 
        }

        public async Task<Magenta1?> FindById(int id)
        {
             return await _context.Magenta1s
                .Include(p => p.InternetPackage)
                .Include(p => p.AppUser)
                .Include(p => p.Magenta1Tariffs)
                .ThenInclude(p => p.Tariff)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Magenta1 entity)
        {
            await _context.Magenta1s.AddAsync(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
