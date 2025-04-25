using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Services
{
    public interface IShoppingCartService
    {
        Task<ShoppingCartItem> AddToCart(AddToCartDto dto);
        Task<ShoppingCart> ActiveShoppingCart(string username);
        Task<ShoppingCartItemDto> RemoveFromCart(int id);
    }
}
