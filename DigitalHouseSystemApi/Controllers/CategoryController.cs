using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Interfaces;
using DigitalHouseSystemApi.Models;
using DigitalHouseSystemApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHouseSystemApi.Controllers
{
    public class CategoryController : BaseApiController
    {
        private readonly ICategoryService _categoryService;
        private readonly IPhotoService _photoService;

        public CategoryController(ICategoryService categoryService, IPhotoService photoService)
        {
            _categoryService = categoryService;
            _photoService = photoService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllCategories(){
            return  Ok(await _categoryService.FindAllCategoriesAsync());
        }

        [HttpPost("add-image")]
        public async Task<ActionResult<PhotoDto>> addPhoto(IFormFile file, int id)
        {
            var category = await _categoryService.FindByIdAsync(id);

            if (category == null) return NotFound();

            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            category.Photo = photo;

            if (await _categoryService.SaveAllAsync()) return Ok(new PhotoDto
            {
                Id = photo.Id,
                Url = photo.Url
            });

            return BadRequest("problem adding photo!");
        }
    }
}
