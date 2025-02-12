using DigitalHouseSystemApi.Interfaces;
using DigitalHouseSystemApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalHouseSystemApi.Data
{
    public class ProblemRepository : IProblemRepository
    {
        private readonly ApplicationDbContext _context;
        public ProblemRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
        public async Task<IEnumerable<Problem>> FindAllAsync()
        {
            return await _context.Problems
                .Include(p => p.Photo)
                .ToListAsync();
        }
    }
}
