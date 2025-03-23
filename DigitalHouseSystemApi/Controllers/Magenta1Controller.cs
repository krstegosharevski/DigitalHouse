using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Models;
using DigitalHouseSystemApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHouseSystemApi.Controllers
{
    public class Magenta1Controller : BaseApiController
    {

        private readonly IMagenta1Service _magenta1Service;

        public Magenta1Controller(IMagenta1Service magenta1Service) 
        {
            _magenta1Service = magenta1Service;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Magenta1Dto>>> GetAll()
        {
            return Ok(await _magenta1Service.GetAllMagenta1());
        }


    }
}
