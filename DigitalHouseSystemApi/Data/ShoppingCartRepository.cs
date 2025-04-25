using DigitalHouseSystemApi.Interfaces;
using DigitalHouseSystemApi.Models;
using DigitalHouseSystemApi.Models.Exceptions;
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

        public async Task<ShoppingCart> FindByUsernameAndStatus(string username, ShoppingCartStatus status)
        {
            var shoppingCart = await _context.ShoppingCarts
                .Include(p => p.Items)
                .ThenInclude(item => item.Product)
                .ThenInclude(photo => photo.Photo)
                .FirstOrDefaultAsync(
            p => p.AppUser.UserName == username && p.Status == status);

            if (shoppingCart == null) throw new ShoppingCartNotFoundException(username);

            return shoppingCart;
        }

        public async Task<ShoppingCart> FindByUsernameAndStatusForAdd(string username, ShoppingCartStatus status)
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

        public ShoppingCartStatus ChangeStatusCancel(string username)
        {
            var shoppingCart =  _context.ShoppingCarts
                .Include(p => p.Items)
                .ThenInclude(item => item.Product)
                .ThenInclude(photo => photo.Photo)
                .FirstOrDefault( p => 
                p.AppUser.UserName == username && 
                p.Status == ShoppingCartStatus.ACTIVE);

            if (shoppingCart == null) throw new ShoppingCartNotFoundException(username);

            shoppingCart.Status = ShoppingCartStatus.CANCALED;
            _context.SaveChanges();

            return shoppingCart.Status;
        }
    }
}
