using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Data.Mappers
{
    public static class Magenta1TariffMapper
    {
        public static Magenta1TariffDto MappToDtoModelM1(this Tariff domainModel)
        {
            return new Magenta1TariffDto()
            {
                Id = domainModel.Id,
                Name = domainModel.Name,
                InternetSpeed = domainModel.InternetSpeed,
                ConversationTime = domainModel.ConversationTime,
                SMS = domainModel.SMS,
                RoamingInternet = domainModel.RoamingInternet,
                InternationalNetworkCalls = domainModel.InternationalNetworkCalls,
                Price = domainModel.Price,
                Discount = domainModel.Discount
            };
        }
    }
}
