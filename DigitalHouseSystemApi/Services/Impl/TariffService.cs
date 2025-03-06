using DigitalHouseSystemApi.Data.Mappers;
using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Interfaces;

namespace DigitalHouseSystemApi.Services.Impl
{
    public class TariffService : ITariffService
    {
        private readonly ITariffRepository _tariffRepository;

        public TariffService(ITariffRepository tariffRepository)
        {
            _tariffRepository = tariffRepository;
        }

      

        public async Task<ICollection<PrepaidTariffDto>> GetAllPrepaidTariffs()
        {
            var tariffs = await _tariffRepository.SelectAllTariffsPrepaidAsync();
            
            ICollection<PrepaidTariffDto> tariffsDto = new List<PrepaidTariffDto>(); 

            foreach (var item in tariffs)
            {
                tariffsDto.Add(item.MappToDtoModelPre());
            }

            return tariffsDto;
        }

        public async Task<ICollection<Trust12TariffDto>> GetAllTrust12Tariffs()
        {
            var tariffs = await _tariffRepository.SelectAllTariffsTrust12Async();

            ICollection<Trust12TariffDto> tariffsDto = new List<Trust12TariffDto>();

            foreach (var item in tariffs)
            {
                tariffsDto.Add(item.MappToDtoModelT12());
            }

            return tariffsDto;
        }

        public async Task<ICollection<NoContractTariffDto>> GetAllNoContractTariffs()
        {
            var tariffs = await _tariffRepository.SelectAllTariffsNoContractAsync();

            ICollection<NoContractTariffDto> tariffsDto = new List<NoContractTariffDto>();

            foreach (var item in tariffs)
            {
                tariffsDto.Add(item.MappToDtoModelNC());
            }

            return tariffsDto;
        }
    }
}
