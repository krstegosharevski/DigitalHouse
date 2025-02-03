using DigitalHouseSystemApi.Data.Mappers;
using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Interfaces;
using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Services.Impl
{
    public class ColorService : IColorService
    {
        private readonly IColorRepository _colorRepository;

        public ColorService(IColorRepository colorRepository)
        {
            _colorRepository = colorRepository;
        }

        public async Task<IEnumerable<ColorDto>> GetColorsAsync()
        {
            var col = await _colorRepository.SelectAllColorsAsync();
            ICollection<ColorDto> colors = new List<ColorDto>(); 
            foreach (var color in col)
            {
                colors.Add(color.MappToDtoModel());
            }
            
            return colors;
        }

        public async Task<ICollection<Color>> GetAllColorsByIdAsync(List<int> colorIds)
        {
            return await _colorRepository.FindAllByIdAsync(colorIds);
        }
    }
}
