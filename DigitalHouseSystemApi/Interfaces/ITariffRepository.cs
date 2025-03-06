using DigitalHouseSystemApi.Migrations;
using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Interfaces
{
    public interface ITariffRepository
    {
        Task<IEnumerable<Tariff>> SelectAllTariffsPrepaidAsync();
        Task<IEnumerable<Tariff>> SelectAllTariffsTrust12Async();
        Task<IEnumerable<Tariff>> SelectAllTariffsNoContractAsync();

    }
}
