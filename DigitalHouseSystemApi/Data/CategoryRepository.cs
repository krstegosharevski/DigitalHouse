using DigitalHouseSystemApi.Interfaces;
using DigitalHouseSystemApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalHouseSystemApi.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) 
        {
             _context = context;
        }

        public async Task<Category> FindByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Category> FindByNameAsync(string name)
        {
            return await _context.Categories.FirstOrDefaultAsync(p => p.Name ==  name);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories
                .Include(p => p.Photo)
                .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
