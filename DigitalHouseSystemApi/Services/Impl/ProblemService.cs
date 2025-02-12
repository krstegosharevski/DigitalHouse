using DigitalHouseSystemApi.Data.Mappers;
using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Interfaces;
using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Services.Impl
{
    public class ProblemService : IProblemService
    {
        private readonly IProblemRepository _problemRepository;

        public ProblemService(IProblemRepository problemRepository)
        {
            _problemRepository = problemRepository;
        }

        public async Task<IEnumerable<ProblemDto>> GetAllProblemsToListAsync()
        {
            var problems = await _problemRepository.FindAllAsync();
            return problems.Select(item => item.MappToDtoModel()).ToList();
        }
    }
}
