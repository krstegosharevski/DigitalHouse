using DigitalHouseSystemApi.Data;
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
        public readonly IPhotoService _photoService;
        public readonly IPerspectiveService _perspectiveService;


        public ProblemService(IProblemRepository problemRepository, 
                              IPhotoService photoService,
                              IPerspectiveService perspectiveService)
        {
            _problemRepository = problemRepository;
            _photoService = photoService;
            _perspectiveService = perspectiveService;
        }

        public async Task<PagedList<ProblemDto>> GetAllProblemsToListAsync(ProblemParams problemParams)
        {
            var problems = await _problemRepository.FindAllAsync(problemParams);
            var problemDtos = problems.Select(item => item.MappToDtoModel()).ToList();

            return new PagedList<ProblemDto>(problemDtos, problems.TotalCount, problemParams.PageNumber, problemParams.PageSize);
        }

        public async Task<ProblemDto> ReportNewProblem(ProblemDto problemDto, IFormFile file)
        {

            var toxicityScore = await _perspectiveService.AnalyzeTextAsync(problemDto.Context);

            if (toxicityScore.HasValue && toxicityScore > 0.65) // Променете ја границата ако е потребно
            {
                throw new Exception("Your report contains toxic language and cannot be submitted.");
            }

            var problem = new Problem(problemDto.Email, problemDto.Name, problemDto.Context);

            _problemRepository.Save(problem);

            
                if (file != null)
                {
                    var success = await AddPhotoAsync(file, problem.Id);
                    if (!success)
                    {
                        throw new Exception("Error adding photo to product");
                    }
                }
         

            return problem.MappToDtoModel();
        }

        private async Task<bool> AddPhotoAsync(IFormFile file, int productId)
        {
            var problem = await _problemRepository.FindByIdAsync(productId);

            if (problem == null) throw new ArgumentException("Error: Problem not found");

            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null) throw new ArgumentException("Error: Can not add photo!");

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };
            problem.Photo = photo;

            if (_problemRepository.SaveChanges()) return true;
            return false;
        }




    }
}
