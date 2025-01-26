namespace DigitalHouseSystemApi.Extensions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(string name)
           : base($"Product with Name: {name} was not found.")
        {
        }
    }
}
