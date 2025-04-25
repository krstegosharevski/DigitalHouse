namespace DigitalHouseSystemApi.Models.Exceptions
{
    public class ShoppingCartItemNotFoundException : Exception
    {
        public ShoppingCartItemNotFoundException(int id)
      : base($"Shopping Cart with Id: {id} was not found.")
        {
        }
    }
}
