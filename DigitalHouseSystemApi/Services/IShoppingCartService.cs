using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Services
{
    public interface IShoppingCartService
    {
        Task<ShoppingCartItem> AddToCart(int productId, string hexCode, string username);
        Task<ShoppingCart> ActiveShoppingCart(string username);
    }
}
