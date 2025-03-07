using DigitalHouseSystemApi.DTOs;

namespace DigitalHouseSystemApi.Services
{
    public interface IInternetPackageService
    {

        Task<ICollection<InternetPackageDto>> GetAllInternetPackagesAsync();
    }
}
