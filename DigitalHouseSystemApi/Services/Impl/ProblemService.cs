using DigitalHouseSystemApi.Data.Mappers;
using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Helpers;
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

        public async Task<PagedList<ProblemDto>> GetAllProblemsToListAsync(ProblemParams problemParams)
        {
            var problems = await _problemRepository.FindAllAsync(problemParams);
            var problemDtos = problems.Select(item => item.MappToDtoModel()).ToList();

            return new PagedList<ProblemDto>(problemDtos, problems.TotalCount, problemParams.PageNumber, problemParams.PageSize);
        }
    }
}
