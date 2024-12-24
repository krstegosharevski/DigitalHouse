using DigitalHouseSystemApi.Interfaces;
using DigitalHouseSystemApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalHouseSystemApi.Data
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ApplicationDbContext _context;
        public BrandRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
        public async Task<Brand> FindByIdAsync(int id)
        {
            return await _context.Brands.FirstOrDefaultAsync(p => p.Id == id);
        }
            
    }
}
