using DigitalHouseSystemApi.DTOs;
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
        public async Task<ActionResult<IEnumerable<BrandDto>>> getAllBrands()
        {
            return Ok(await _brandService.GetAllBrandsAsync());
        }
    }
}
