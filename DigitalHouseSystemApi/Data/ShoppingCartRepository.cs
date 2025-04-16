using DigitalHouseSystemApi.Interfaces;
using DigitalHouseSystemApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalHouseSystemApi.Data
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;

        public ShoppingCartRepository(ApplicationDbContext context,
            IUserRepository userRepository) 
        { 
            _context = context; 
            _userRepository = userRepository;
        }
        public async Task<ShoppingCart> FindByIdAsync(int id)
        {
            return await _context.ShoppingCarts.FirstOrDefaultAsync(p => p.Id == id);
        }

        //Kreiraj ako nemat!
        public async Task<ShoppingCart> FindByUsernameAndStatus(string username, ShoppingCartStatus status)
        {
            var shoppingCart = await _context.ShoppingCarts
                .Include(p => p.Items)
                .ThenInclude(item => item.Product)
                .ThenInclude(photo => photo.Photo)
                .FirstOrDefaultAsync(
            p => p.AppUser.UserName == username && p.Status == status);

            if (shoppingCart == null)
            {
                AppUser user = await _userRepository.GetUserByUsername(username);
                return await Save(new ShoppingCart(user));
            }

            return shoppingCart;
        }

        public async Task<ShoppingCart> Save(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Add(shoppingCart);
            await _context.SaveChangesAsync();
            return shoppingCart;
        }

        public async Task UpdateAsync(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Update(shoppingCart);
            await _context.SaveChangesAsync();
        }
    }
}
