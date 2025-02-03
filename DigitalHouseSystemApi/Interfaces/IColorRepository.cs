using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Interfaces
{
    public interface IColorRepository
    {
        Task<Color> GetColorByNameAsync(string name);
        Task<IEnumerable<Color>> SelectAllColorsAsync();
        Task<ICollection<Color>> FindAllByIdAsync(List<int> colorIds);
    }
}
