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

        [HttpPost("report-problem")]
        public async Task<ActionResult<ProblemDto>> ReportProblem([FromForm] ProblemDto problem, [FromForm] IFormFile? file = null)
        {
            if (problem == null)
            {
                return BadRequest("You need to enter a problem!");
            }
            try
            {
                return Ok(await _problemService.ReportNewProblem(problem, file));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }


        }


    }
}
