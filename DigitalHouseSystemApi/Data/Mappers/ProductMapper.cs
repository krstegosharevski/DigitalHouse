using System.Xml.Linq;
using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Data.Mappers
{
    public static class ProductMapper
    {
        public static ProductDto MappToDtoModel(this Product domainModel)
        {
            if (domainModel == null) { throw new Exception(); }
            return new ProductDto()
            {
                Id = domainModel.Id,
                Name = domainModel.Name,
                Price = domainModel.Price,
                Description = domainModel.Description,
                IsPresent = domainModel.IsPresent,
                CreatedAt = domainModel.CreatedAt,
                PhotoUrl = domainModel.Photo?.Url,
                CategoryName = domainModel.Category.Name,
                BrandName = domainModel.Brand.Name
            };
        }
    }
}

