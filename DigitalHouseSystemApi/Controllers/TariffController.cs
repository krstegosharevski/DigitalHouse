using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHouseSystemApi.Controllers
{
    public class TariffController : BaseApiController
    {
        private readonly ITariffService _tariffService;

        public TariffController(ITariffService tariffService)
        {
            _tariffService = tariffService;
        }

        [HttpGet("prepaid")]
        public async Task<ActionResult<ICollection<PrepaidTariffDto>>> GetPrepaidTariffs() 
        {
            return  Ok(await _tariffService.GetAllPrepaidTariffs());
        }

        [HttpGet("trust12")]
        public async Task<ActionResult<ICollection<Trust12TariffDto>>> GetTrust12Tariffs() 
        { 
            return  Ok(await _tariffService.GetAllTrust12Tariffs());
        }

        [HttpGet("no-contract")]
        public async Task<ActionResult<ICollection<NoContractTariffDto>>> GetNoContracTariffs() 
        { 
            return  Ok(await _tariffService.GetAllNoContractTariffs());
        }

        [HttpGet("magenta1")]
        public async Task<ActionResult<ICollection<Magenta1TariffDto>>> GetMagenta1Tariffs()
        {
            return Ok(await _tariffService.GetAllMagenta1Tariffs());
        }
    }
}
