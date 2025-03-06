using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Data.Mappers
{
    public static class PrepaidTariffMapper
    {
        public static PrepaidTariffDto MappToDtoModelPre(this Tariff domainModel)
        {
            return new PrepaidTariffDto()
            {
                Id = domainModel.Id,
                Name = domainModel.Name,
                InternetSpeed = domainModel.InternetSpeed,
                ConversationTime = domainModel.ConversationTime,
                SMS = domainModel.SMS,
                RoamingInternet = domainModel.RoamingInternet,
                Price = domainModel.Price,
            };
        }
    }
}
