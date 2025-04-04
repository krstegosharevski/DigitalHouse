using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Models;
using DigitalHouseSystemApi.Models.Exceptions;
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

        [HttpPut("approve/{id}")]
        public async Task<ActionResult<Magenta1Dto>> ApproveMagenta1(int id)
        {
            try
            {
                return Ok(await _magenta1Service.UpdateStatusOnMagenta1(id));
            }
            catch (Magenta1NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (CantApproveNewMagenta1 ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<Magenta1Dto>> CreateMagenta1(CreateMagenta1Dto magentaToCreate)
        {
            try
            {
                return Ok(await _magenta1Service.CreateMagenta1(magentaToCreate));
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
