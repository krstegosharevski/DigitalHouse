namespace DigitalHouseSystemApi.DTOs
{
    public class AddProductDto
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public bool IsPresent { get; set; }
        public int Quantity  { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }

        //public List<int> ColorIds { get; set; } = new List<int>();
        public List<ProductColorDto> ProductColors { get; set; }
    }
}
