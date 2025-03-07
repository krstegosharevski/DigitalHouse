using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHouseSystemApi.Controllers
{
    public class InternetPackageController : BaseApiController
    {
        private readonly IInternetPackageService _internetPackageService;
        public InternetPackageController(IInternetPackageService internetPackageService)
        {
            _internetPackageService = internetPackageService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<InternetPackageDto>>> GetAllPackages()
        {
            return Ok(await _internetPackageService.GetAllInternetPackagesAsync());
        }
    }
}
