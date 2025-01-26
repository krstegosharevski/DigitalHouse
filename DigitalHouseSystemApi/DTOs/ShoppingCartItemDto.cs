namespace DigitalHouseSystemApi.DTOs
{
    public class ShoppingCartItemDto
    {
        public string Name { get; set; }
        public int TotalQuantity { get; set; }
        public string? HexCode { get; set; }

        public ShoppingCartItemDto() { }

        public ShoppingCartItemDto(string name, int totalQuantity)
        {
            Name = name;
            TotalQuantity = totalQuantity;
        }

    }
}
