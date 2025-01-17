using System.Runtime.CompilerServices;
using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Data.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryDto MappToDtoModel(this Category domainModel)
        {
            if (domainModel == null) { throw new Exception(); }
            return new CategoryDto()
            {
                Name = domainModel.Name,
                PhotoUrl = domainModel.Photo?.Url
            };
        }
    }
}
