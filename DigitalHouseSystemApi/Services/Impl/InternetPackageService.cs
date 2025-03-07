using DigitalHouseSystemApi.Data.Mappers;
using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Interfaces;

namespace DigitalHouseSystemApi.Services.Impl
{
    public class InternetPackageService : IInternetPackageService
    {
        private readonly IInternetPackageRepository _internetPackageRepository;

        public InternetPackageService(IInternetPackageRepository internetPackageRepository)
        {
            _internetPackageRepository = internetPackageRepository;
        }

        public async Task<ICollection<InternetPackageDto>> GetAllInternetPackagesAsync()
        {
            var packages = await _internetPackageRepository.SelectAllInternetPackagesAsync();

            return packages.Select(package => package.MappToDtoModel()).ToList();
        }
    }
}
