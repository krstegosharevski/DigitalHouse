using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Models;
using DigitalHouseSystemApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHouseSystemApi.Controllers
{
    public class ProblemController : BaseApiController
    {
        private readonly IProblemService _problemService;

        public ProblemController(IProblemService problemService)
        {
            _problemService = problemService;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProblemDto>>> GetAllProblems() {
            return Ok(await _problemService.GetAllProblemsToListAsync());
        }


    }
}
