namespace DigitalHouseSystemApi.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public int TotalQuantity { get; set; }
        public string? HexCode { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

        public ShoppingCartItem() { }
        public ShoppingCartItem(Product product)
        {
            Product = product;
            TotalQuantity = 1;
        }
    }
}
