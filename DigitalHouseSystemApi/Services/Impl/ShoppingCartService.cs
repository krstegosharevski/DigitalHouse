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

        // Neznam kolku e dobro implementirano...
        public async Task<ShoppingCartItem> AddToCart(ProductDto productDto, string username)
        {
            var product = await _productRepository.FindProductByName(productDto.Name);
            if (product == null) { throw new ProductNotFoundException(productDto.Name); }

            
            ShoppingCartItem item = new ShoppingCartItem(product);

            var shoppingCart = await _shoppingCartRepository.FindByUsernameAndStatus(username, ShoppingCartStatus.ACTIVE);

            shoppingCart.Items.Add(item);

            await _shoppingCartRepository.UpdateAsync(shoppingCart);

            return item;
        }
         
    }
}
