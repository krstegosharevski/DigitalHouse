using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Models.Exceptions;
using DigitalHouseSystemApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHouseSystemApi.Controllers
{
    public class BrandController : BaseApiController
    {
        public readonly IBrandService _brandService;

        public BrandController(IBrandService brandService) 
        { 
            _brandService = brandService;  
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
        {
            return Ok(await _brandService.GetAllBrandsAsync());
        }

        [HttpGet("by-category")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrandsForCategory([FromQuery] string categoryName)
        {
            try
            {
                return Ok(await _brandService.GetBrandsForGivenCategory(categoryName));
            } catch (CategoryNotFoundException ex) {
                return NotFound(ex.Message);
            } catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }
    }
}
