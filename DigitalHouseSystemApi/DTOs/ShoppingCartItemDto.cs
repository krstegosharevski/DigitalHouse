namespace DigitalHouseSystemApi.DTOs
{
    public class ShoppingCartItemDto
    {
        public string Name { get; set; }
        public int TotalQuantity { get; set; }
        public string? HexCode { get; set; }
        public string PhotoUrl { get; set; }
        public double Price { get; set; }

        public ShoppingCartItemDto() { }

        public ShoppingCartItemDto(string name, int totalQuantity, string photoUrl, double price)
        {
            Name = name;
            TotalQuantity = totalQuantity;
            PhotoUrl = photoUrl;
            Price = price;
        }

    }
}
