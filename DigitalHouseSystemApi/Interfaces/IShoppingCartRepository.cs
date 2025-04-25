using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Interfaces
{
    public interface IShoppingCartRepository
    {
        
        Task<ShoppingCart> FindByIdAsync(int id);
        
        Task<ShoppingCart> FindByUsernameAndStatus(string username, ShoppingCartStatus status);
        
        //save
        Task<ShoppingCart> Save(ShoppingCart shoppingCart);

        Task UpdateAsync(ShoppingCart shoppingCart);
        Task<ShoppingCartStatus> ChangeStatusCancel(string username);


    }
}
