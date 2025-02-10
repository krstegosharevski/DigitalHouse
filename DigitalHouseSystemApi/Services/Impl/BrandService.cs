using DigitalHouseSystemApi.Data.Mappers;
using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Interfaces;
using DigitalHouseSystemApi.Models.Exceptions;

namespace DigitalHouseSystemApi.Services.Impl
{
    public class BrandService : IBrandService
    {

        public readonly IBrandRepository _brandRepository;
        public readonly ICategoryService _categoryService;
        
        public BrandService(IBrandRepository brandRepository, ICategoryService categoryService) 
        { 
            _brandRepository = brandRepository;
            _categoryService = categoryService;
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

        public async Task<IEnumerable<BrandDto>> GetBrandsForGivenCategory(string categoryName)
        {
            try
            {
                var category = await _categoryService.FindByNameAsync(categoryName);
                List<BrandDto> brandsDto = new List<BrandDto>();

                var brands = await _brandRepository.GetAllBrandsByProductCategoryAsync(category.Id);

                foreach (var brand in brands)
                {
                    brandsDto.Add(brand.MappToDtoModel());
                }
                return brandsDto;

            }
            catch(CategoryNotFoundException)
            {
                throw;
            }
        }
    }
}
