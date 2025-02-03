using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Data.Mappers
{
    public static class BrandMapper
    {
        public static BrandDto MappToDtoModel(this Brand domainModel)
        {
            if (domainModel == null) { throw new Exception(); }
            return new BrandDto()
            {
                Id = domainModel.Id,
                Name = domainModel.Name
            };
        }
    }
}
