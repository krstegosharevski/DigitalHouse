namespace DigitalHouseSystemApi.Models.Exceptions
{
    public class BrandNotFoundException : Exception
    {
        public BrandNotFoundException(int id)
          : base($"Brand with ID: {id} was not found.")
        {
        }
    }
}
