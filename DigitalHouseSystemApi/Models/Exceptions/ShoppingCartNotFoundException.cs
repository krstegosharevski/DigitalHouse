namespace DigitalHouseSystemApi.Models.Exceptions
{
    public class ShoppingCartNotFoundException : Exception
    {
        public ShoppingCartNotFoundException(string name)
          : base($"Shopping Cart with Name: {name} was not found.")
        {
        }
    }
}
