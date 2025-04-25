using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Data.Mappers
{
    public static class ShoppingCartItemMapper
    {
        public static ShoppingCartItemDto MappToDtoModel(this ShoppingCartItem domainModel)
        {
            if (domainModel == null) { throw new Exception(); }
            return new ShoppingCartItemDto()
            {
                Id = domainModel.Id,
                Name = domainModel.Product.Name,
                TotalQuantity = domainModel.TotalQuantity,
                HexCode = domainModel.HexCode,
                PhotoUrl = domainModel.Product.Photo.Url,
                Price = domainModel.Product.Price
            };
        }
    }
}
