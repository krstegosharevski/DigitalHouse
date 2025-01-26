using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Services
{
    public interface IShoppingCartService
    {
        Task<ShoppingCartItem> AddToCart(ProductDto productDto, string username);
        Task<ShoppingCart> ActiveShoppingCart(string username);
    }
}
