using DigitalHouseSystemApi.Data.Mappers;
using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Interfaces;
using DigitalHouseSystemApi.Models;
using DigitalHouseSystemApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHouseSystemApi.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;
        private readonly IPhotoService _photoService;
        
        public ProductController(IProductRepository productRepository, IProductService productService , IPhotoService photoService)
        {
            _productRepository = productRepository;
            _productService = productService;
            _photoService = photoService;
        }

        //Okej metod
        [HttpPost("add-image")]
        public async Task<ActionResult<PhotoDto>> addPhoto(IFormFile file, int id)
        {
            var product = await _productService.FindByIdAsync(id);

            if (product == null) return NotFound();

            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            product.Photo = photo;

            if (await _productRepository.SaveAllAsync()) return Ok(new PhotoDto
            {
                Id = photo.Id,
                Url = photo.Url
            });

            return BadRequest("problem adding photo!");
            //try
            //{
            //    var success = await _productService.AddPhotoAsync(file, id);

            //    if (success)
            //    {
            //        return Ok();
            //    }

            //    return StatusCode(500, "Error: Could not save photo.");
            //}
            //catch (ArgumentException ex)
            //{
            //    return NotFound(ex.Message);
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}
        }

        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> deletePhoto(int productId)
        {
            if (productId == 0) return NotFound();

            try
            {
                await _photoService.DeletePhotoAsync(productId);

                await _productService.DeletePhoto(productId);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add-product")]
        public async Task<ActionResult<ProductDto>> AddProduct([FromForm] AddProductDto productDto, [FromForm] IFormFile file)
        {
            if (productDto == null)
            {
                return BadRequest("Invalid product data");
            }

            try
            {
                // thorough the service; adding.
                var addedProduct = await _productService.AddProductAsync(productDto, file);
               
                return Ok(addedProduct);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("get-all")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> getAllProducts()
        {
            IEnumerable<Product> products = await _productService.GetAllProductsAsync();
            
            if(products == null) return NotFound();

            List<ProductDto> productsDto = new List<ProductDto>();

            foreach (var product in products)
            {
                productsDto.Add(product.MappToDtoModel());
            }

            return Ok(productsDto);
        }


    }
}
