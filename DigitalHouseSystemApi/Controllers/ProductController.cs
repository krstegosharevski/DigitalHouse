using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Interfaces;
using DigitalHouseSystemApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHouseSystemApi.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly IProductRepository _productRepository;
        private readonly IPhotoService _photoService;  
        public ProductController(IProductRepository productRepository, IPhotoService photoService) 
        {
            _productRepository = productRepository;
            _photoService = photoService;
        }

        [HttpPost("add-image")]
        public async Task<ActionResult<PhotoDto>> addPhoto(IFormFile file, int id)
        {
            var product = await _productRepository.FindByIdAsync(id);

            if(product == null) return NotFound();

            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            product.Photo = photo;

            if(await _productRepository.SaveAllAsync()) return Ok(new PhotoDto
            {
                Id = photo.Id,  
                Url = photo.Url 
            });

            return BadRequest("Problem adding photo!");
        }
    }
}
