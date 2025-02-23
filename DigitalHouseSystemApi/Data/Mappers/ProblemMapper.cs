using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Data.Mappers
{
    public static class ProblemMapper
    {
        public static ProblemDto MappToDtoModel(this Problem domainModel)
        {
            if (domainModel == null) { throw new Exception(); }
            return new ProblemDto()
            {
                Id = domainModel.Id,
                Email = domainModel.Email,
                Name = domainModel.Name,
                Context = domainModel.Context,
                CreatedAt = domainModel.CreatedAt,
                PhotoUrl = domainModel.Photo?.Url
            };
        }
    }
}
