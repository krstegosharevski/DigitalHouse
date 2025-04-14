namespace DigitalHouseSystemApi.DTOs
{
    public class ProductColorDto
    {
        public int ColorId { get; set; }
        public int Quantity { get; set; }

        public ProductColorDto() { }

        public ProductColorDto(int colorId, int quantity) 
        {
            ColorId = colorId;
            Quantity = quantity;
        }
    }
}
