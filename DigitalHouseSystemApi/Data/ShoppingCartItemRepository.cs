using DigitalHouseSystemApi.Interfaces;
using DigitalHouseSystemApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalHouseSystemApi.Data
{
    public class ShoppingCartItemRepository : IShoppingCartItemRepository
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ShoppingCartItem> DeleteItem(int id)
        {
            var item = await _context.ShoppingCartItems
                .Include(x => x.Product)
                .ThenInclude(p => p.Photo)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
            {
                return null;
            }

            _context.ShoppingCartItems.Remove(item);
            await _context.SaveChangesAsync();

            return item;
        }
    }
}
