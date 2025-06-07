using DigitalHouseSystemApi.Data.Mappers;
using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Models;
using DigitalHouseSystemApi.Models.Exceptions;
using DigitalHouseSystemApi.Services;
using DigitalHouseSystemApi.Services.Impl;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHouseSystemApi.Controllers
{
    public class ShoppingCartController : BaseApiController
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ILemonSqueezyService _lemonSqueezyService;
        public ShoppingCartController(IShoppingCartService shoppingCartService, ILemonSqueezyService lemonSqueezyService) 
        {
            _shoppingCartService = shoppingCartService;
            _lemonSqueezyService = lemonSqueezyService;
        }

        [HttpPost("add-to-cart")]
        public async Task<ActionResult<ShoppingCartItemDto>> АddProdductToShoppingCart([FromBody] AddToCartDto dto)
        {
            try
            {
                var item = await _shoppingCartService.AddToCart(dto);
                return Ok(item.MappToDtoModel());
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-shoppingcart")]
        public async Task<ActionResult<IEnumerable<ShoppingCartItemDto>>> GetProductsInShoppingCard(string username)
        {
            try
            {
                var shoppingCart = await _shoppingCartService.ActiveShoppingCart(username);
                ICollection<ShoppingCartItemDto> sp = new List<ShoppingCartItemDto>();
                
                foreach (var item in shoppingCart.Items)
                {
                    ShoppingCartItemDto i = item.MappToDtoModel();
                    sp.Add(i);
                }

                return Ok(sp);
            }
            catch (ShoppingCartNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("remove-item")]
        public async Task<ActionResult<ShoppingCartItemDto>> removeFromShoppingCart([FromQuery] int itemId)
        {
            try
            {
                var shoppingCartItemDto = await _shoppingCartService.RemoveFromCart(itemId);
                return Ok(shoppingCartItemDto);
            }
            catch (ShoppingCartItemNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("cancel-status")]
        public ActionResult<bool> cancelStatusCart(string username)
        {
            if (username == null) return BadRequest(false);

            try
            {
                var deleted =  _shoppingCartService.CancelStatus(username);
                return Ok(deleted);
            }
            catch (ShoppingCartNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Buy the shopping cart; so the cart will go status COMPLETED

        // Loginc to buy
        [HttpPost("create-payment")]
        public async Task<IActionResult> CreatePayment([FromQuery] string username)
        {
            try
            {
                var cart = await _shoppingCartService.ActiveShoppingCart(username);

                if (cart == null || !cart.Items.Any())
                    return BadRequest("Cart is empty");

                var totalAmount = cart.Items.Sum(x => x.TotalQuantity * x.Product.Price);

                var checkoutUrl =  await _lemonSqueezyService.CreateCheckoutAsync("test@example.com", (decimal)totalAmount, username);
                //var checkoutUrl = "shhshs";

                return Ok(new { url = checkoutUrl });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
