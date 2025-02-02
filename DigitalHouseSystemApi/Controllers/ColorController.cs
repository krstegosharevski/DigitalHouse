using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Models;
using DigitalHouseSystemApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHouseSystemApi.Controllers
{
    public class ColorController : BaseApiController
    {
        private readonly IColorService _colorService;

        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ColorDto>>> GetAllColors()
        {
            return Ok(await _colorService.GetColorsAsync());
        }
    }
}
