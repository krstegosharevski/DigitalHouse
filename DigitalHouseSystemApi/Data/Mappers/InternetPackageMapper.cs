using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalHouseSystemApi.Data.Mappers
{
    public static class InternetPackageMapper
    {
        public static InternetPackageDto MappToDtoModel(this InternetPackage domainModel)
        {
            return new InternetPackageDto()
            {
                Id = domainModel.Id,
                Name = domainModel.Name,
                InternetSpeed = domainModel.InternetSpeed,
                ConversationTime = domainModel.ConversationTime,
                MagentaTV = domainModel.MagentaTV,
                MagentaTV_GO = domainModel.MagentaTV_GO,
                Functions = domainModel.Functions,
                Price = domainModel.Price,
                Discount = domainModel.Discount
            };
        }
    }
}
