using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.DTOs
{
    public class GetProductEditDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public bool IsPresent { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? PhotoUrl { get; set; }
        public int Category { get; set; }
        public int Brand { get; set; }
        //public ICollection<int>? Colors { get; set; }
        public List<ProductColorDto> ProductColors { get; set; } = new List<ProductColorDto>();

        public GetProductEditDto() { }  

        public GetProductEditDto(int id, string name, double price, string description, bool is_present, string photo_url, int category, int brand, List<ProductColorDto> colors)
        {
            Id = id;
            Name = name;
            Price = price;
            Description = description;
            IsPresent = is_present;
            PhotoUrl = photo_url;
            Category = category;
            Brand = brand;
            ProductColors = colors;
        }

    }
}
