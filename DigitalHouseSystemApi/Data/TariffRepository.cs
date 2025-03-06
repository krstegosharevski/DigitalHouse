using DigitalHouseSystemApi.Interfaces;
using DigitalHouseSystemApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalHouseSystemApi.Data
{
    public class TariffRepository : ITariffRepository
    {
        private readonly ApplicationDbContext _context;

        public TariffRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tariff>> SelectAllTariffsPrepaidAsync()
        {
            return await _context.Tariffs
                .Include(p => p.TariffType)
                .Where(p => p.TariffType != null && p.TariffType.TariffCategory == TariffCategory.PREPAID)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tariff>> SelectAllTariffsTrust12Async()
        {
            return await _context.Tariffs
               .Include(p => p.TariffType)
               .Where(p => p.TariffType.Name == ("Trust 12"))
               .ToListAsync();
        }

        public async Task<IEnumerable<Tariff>> SelectAllTariffsNoContractAsync()
        {
            return await _context.Tariffs
              .Include(p => p.TariffType)
              .Where(p => p.TariffType.Name == ("Without a contract"))
              .ToListAsync();
        }

    }
}
