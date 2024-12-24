namespace DigitalHouseSystemApi.DTOs
{
    public class ProductDto
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public bool IsPresent { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? PhotoUrl { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }

        public ProductDto() { }

        public ProductDto(string name, double price, string description, bool is_present, string photo_url, string category_name, string brand_name)
        {
            Name = name;
            Price = price;
            Description = description;
            IsPresent = is_present;
            PhotoUrl = photo_url;
            CategoryName = category_name;
            BrandName = brand_name;
        }
    }
}
