using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Helpers;
using DigitalHouseSystemApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHouseSystemApi.Services
{
    public interface IProblemService
    {
        Task<PagedList<ProblemDto>> GetAllProblemsToListAsync(ProblemParams problemParams);
        Task<ProblemDto> ReportNewProblem(ProblemDto problemDto, IFormFile file);
    }
}
