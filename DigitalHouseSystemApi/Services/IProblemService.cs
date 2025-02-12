using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Services
{
    public interface IProblemService
    {
        Task<IEnumerable<ProblemDto>> GetAllProblemsToListAsync();
    }
}
