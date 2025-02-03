namespace DigitalHouseSystemApi.Models.Exceptions
{
    public class CategoryNotFoundException : Exception
    {
        public CategoryNotFoundException(int id)
          : base($"Category with ID: {id} was not found.")
        {
        }
    }
}
