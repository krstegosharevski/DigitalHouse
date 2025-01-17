using DigitalHouseSystemApi.Data.Mappers;
using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Interfaces;

namespace DigitalHouseSystemApi.Services.Impl
{
    public class BrandService : IBrandService
    {

        public readonly IBrandRepository _brandRepository;
        
        public BrandService(IBrandRepository brandRepository) 
        { 
            _brandRepository = brandRepository;
        }

        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            List<BrandDto> brandsDto = new List<BrandDto>();
            var brands = await _brandRepository.GetAllBrandsAsync();

            foreach (var brand in brands) 
            {
                brandsDto.Add(brand.MappToDtoModel());  
            }
            return brandsDto;
        }
    }
}
