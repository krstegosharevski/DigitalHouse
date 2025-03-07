using DigitalHouseSystemApi.Interfaces;
using DigitalHouseSystemApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalHouseSystemApi.Data
{
    public class InternetPackageRepository : IInternetPackageRepository
    {
        private readonly ApplicationDbContext _context;
        public InternetPackageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InternetPackage>> SelectAllInternetPackagesAsync()
        {
            return await _context.InternetPackages.ToListAsync();
        }
    }
}
