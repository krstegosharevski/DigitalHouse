using System.Collections.Immutable;
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
                        .OrderByDescending(p => p.CreatedAt)
                        .AsQueryable();

            return await PagedList<Problem>.CreateAsync(query, problemParams.PageNumber, problemParams.PageSize);
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Save(Problem problem)
        {
            _context.Problems.Add(problem);
            _context.SaveChanges(); 
        }

        public async Task<Problem?> FindByIdAsync(int id)
        {
            return await _context.Problems
                        .Include(p => p.Photo)
                        .FirstOrDefaultAsync(p => p.Id == id);
        }

        public void DeleteById(Problem problem)
        {
            _context.Problems.Remove(problem);
        }
    }
}
