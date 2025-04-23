namespace DigitalHouseSystemApi.DTOs
{
    public class AddToCartDto
    {
        public int ProductId { get; set; }
        public string HexCode { get; set; }
        public string Username { get; set; }
        
        public AddToCartDto() { }

        public AddToCartDto(int productId, string hexCode, string username)
        {
            ProductId = productId;
            HexCode = hexCode;
            Username = username;
        }
    }
}
