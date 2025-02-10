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

        public async Task<IEnumerable<Brand>> GetAllBrandsAsync()
        {
            return await _context.Brands.ToListAsync();
        }

        public async Task<IEnumerable<Brand>> GetAllBrandsByProductCategoryAsync(int categoryId)
        {
            return await _context.Brands
                .FromSqlInterpolated($@"
                SELECT DISTINCT b.* 
                FROM Brands b 
                INNER JOIN Products p ON b.Id = p.BrandId 
                WHERE p.CategoryId = {categoryId}")
                .ToListAsync();

        }
    }
}
