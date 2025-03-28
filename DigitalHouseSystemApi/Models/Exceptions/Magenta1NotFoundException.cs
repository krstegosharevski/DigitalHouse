namespace DigitalHouseSystemApi.Models.Exceptions
{
    public class Magenta1NotFoundException : Exception
    {
        public Magenta1NotFoundException(int id)
          : base($"Magenta1 with ID: {id} was not found.")
        {
        }
    }
}
