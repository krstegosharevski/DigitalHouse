namespace DigitalHouseSystemApi.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ShoppingCartStatus Status { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

        public ShoppingCart() { }
        public ShoppingCart(AppUser user)
        {
            AppUser = user;
            Status = ShoppingCartStatus.ACTIVE;
        }

    }
}
