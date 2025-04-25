using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Interfaces
{
    public interface IShoppingCartRepository
    {
        
        Task<ShoppingCart> FindByIdAsync(int id);
        
        Task<ShoppingCart> FindByUsernameAndStatus(string username, ShoppingCartStatus status);
        Task<ShoppingCart> FindByUsernameAndStatusForAdd(string username, ShoppingCartStatus status);
        Task<ShoppingCart> Save(ShoppingCart shoppingCart);

        Task UpdateAsync(ShoppingCart shoppingCart);
        ShoppingCartStatus ChangeStatusCancel(string username);


    }
}
