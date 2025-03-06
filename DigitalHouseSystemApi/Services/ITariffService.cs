using DigitalHouseSystemApi.DTOs;

namespace DigitalHouseSystemApi.Services
{
    public interface ITariffService
    {
        Task<ICollection<PrepaidTariffDto>> GetAllPrepaidTariffs();
        Task<ICollection<Trust12TariffDto>> GetAllTrust12Tariffs();
        Task<ICollection<NoContractTariffDto>> GetAllNoContractTariffs();

    }
}
