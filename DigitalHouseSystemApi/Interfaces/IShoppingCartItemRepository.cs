using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Interfaces
{
    public interface IShoppingCartItemRepository
    {
        Task<ShoppingCartItem> DeleteItem(int id);
    }
}
