using DigitalHouseSystemApi.Interfaces;
using DigitalHouseSystemApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalHouseSystemApi.Data
{
    public class ColorRepository : IColorRepository
    {
        private readonly ApplicationDbContext _context;

        public ColorRepository(ApplicationDbContext context)
        {
            _context = context; 
        }

        public async Task<Color> GetColorByNameAsync(string name)
        {
            return await _context.Colors.FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task<IEnumerable<Color>> SelectAllColorsAsync()
        {
            return await _context.Colors.ToListAsync();
        }

        public async Task<ICollection<Color>> FindAllByIdAsync(List<int> colorIds)
        {
            return await _context.Colors.Where(c => colorIds.Contains(c.Id)).ToListAsync();
        }
    }
}
