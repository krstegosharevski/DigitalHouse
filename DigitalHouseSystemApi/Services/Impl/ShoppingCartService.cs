using DigitalHouseSystemApi.Data.Mappers;
using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Interfaces;
using DigitalHouseSystemApi.Models;
using DigitalHouseSystemApi.Models.Exceptions;

namespace DigitalHouseSystemApi.Services.Impl
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository,
                                    IUserRepository userRepository,
                                    IProductRepository productRepository,
                                    IShoppingCartItemRepository shoppingCartItemRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _shoppingCartItemRepository = shoppingCartItemRepository;
        }

        public async Task<ShoppingCart> ActiveShoppingCart(string username)
        {
            var shoppingcard = await _shoppingCartRepository.FindByUsernameAndStatus(username, ShoppingCartStatus.ACTIVE);
            if (shoppingcard == null)
            {
                throw new ShoppingCartNotFoundException(username);
            }

            return shoppingcard;
        }

        public async Task<ShoppingCartItem> AddToCart(AddToCartDto dto)
        {
            var product = await _productRepository.FindByIdAsync(dto.ProductId);
            if (product == null) { throw new ProductNotFoundException(product.Name); }


            ShoppingCartItem item = new ShoppingCartItem(product,dto.HexCode);

            var shoppingCart = await _shoppingCartRepository.FindByUsernameAndStatus(dto.Username, ShoppingCartStatus.ACTIVE);

            shoppingCart.Items.Add(item);

            await _shoppingCartRepository.UpdateAsync(shoppingCart);

            return item;
        }

        public async Task<ShoppingCartItemDto> RemoveFromCart(int id)
        {
            var item = await _shoppingCartItemRepository.DeleteItem(id);
            
            if (item == null) throw new ShoppingCartItemNotFoundException(id);

            return item.MappToDtoModel();
        }

        public async Task<bool> CancelStatus(string username)
        {
            var status = await _shoppingCartRepository.ChangeStatusCancel(username);
            
            if (status == ShoppingCartStatus.CANCALED)
            {
               return true;
            }

            return false;
        }
    }
}
