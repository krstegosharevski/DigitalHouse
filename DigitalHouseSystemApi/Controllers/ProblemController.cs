using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Extensions;
using DigitalHouseSystemApi.Helpers;
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
        public async Task<ActionResult<PagedList<ProblemDto>>> GetAllProblems([FromQuery] ProblemParams problemParams) {
            var problems = await _problemService.GetAllProblemsToListAsync(problemParams);
            Response.AddPaginationHeader(new PaginationHeader(problems.CurrentPage, problems.PageSize, problems.TotalCount, problems.TotalPages));

            return Ok(problems);

        }


    }
}
