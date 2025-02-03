using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Data.Mappers
{
    public static class ColorMapper
    {
        public static ColorDto MappToDtoModel(this Color domainModel)
        {
            if (domainModel == null) { throw new Exception(); }
            return new ColorDto()
            {
                Id = domainModel.Id,
                Name = domainModel.Name,
                HexCode = domainModel.HexCode
            };
        }
    }
}
