using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Data.Mappers
{
    public static class Trust12TariffMapper
    {
        public static Trust12TariffDto MappToDtoModelT12(this Tariff domainModel)
        {
            return new Trust12TariffDto()
            {
                Id = domainModel.Id,
                Name = domainModel.Name,
                InternetSpeed = domainModel.InternetSpeed,
                ConversationTime = domainModel.ConversationTime,
                SMS = domainModel.SMS,
                RoamingInternet = domainModel.RoamingInternet,
                InternationalNetworkCalls = domainModel.InternationalNetworkCalls,
                E_bill = domainModel.E_bill,
                Price = domainModel.Price,
                Discount = domainModel.Discount
            };
        }
    }
}
