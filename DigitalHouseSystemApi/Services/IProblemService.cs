using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Helpers;
using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Services
{
    public interface IProblemService
    {
        Task<PagedList<ProblemDto>> GetAllProblemsToListAsync(ProblemParams problemParams);
    }
}
