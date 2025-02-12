using DigitalHouseSystemApi.Helpers;
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
        public async Task<PagedList<Problem>> FindAllAsync(ProblemParams problemParams)
        {
            var query =  _context.Problems
                        .Include(p => p.Photo)
                        .AsQueryable();

            return await PagedList<Problem>.CreateAsync(query, problemParams.PageNumber, problemParams.PageSize);
        }
    }
}
