using DigitalHouseSystemApi.Migrations;
using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Interfaces
{
    public interface ITariffRepository
    {
        Task<IEnumerable<Tariff>> SelectAllTariffsPrepaidAsync();
        Task<IEnumerable<Tariff>> SelectAllTariffsTrust12Async();
        Task<IEnumerable<Tariff>> SelectAllTariffsNoContractAsync();
        Task<IEnumerable<Tariff>> SelectAllTariffsMagenta1Async();
        Task<Tariff?> FindByIdAsync(int id);


    }
}
