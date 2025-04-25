using DigitalHouseSystemApi.Data.Mappers;
using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Models;
using DigitalHouseSystemApi.Models.Exceptions;
using DigitalHouseSystemApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHouseSystemApi.Controllers
{
    public class ShoppingCartController : BaseApiController
    {
        private readonly IShoppingCartService _shoppingCartService;
        public ShoppingCartController(IShoppingCartService shoppingCartService) 
        {
            _shoppingCartService = shoppingCartService;
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

        // Cancel the shopping cart; so the cart will go status CANCALED
        [HttpPut("cancel-status")]
        public async Task<ActionResult<bool>> cancelStatusCart(string username)
        {
            if (username == null) return BadRequest(false);

            try
            {
                var deleted = await _shoppingCartService.CancelStatus(username);
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


    }
}
