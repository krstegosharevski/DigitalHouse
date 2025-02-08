namespace DigitalHouseSystemApi.Models.Exceptions
{
    public class CategoryNotFoundException : Exception
    {
        public CategoryNotFoundException(int id)
          : base($"Category with ID: {id} was not found.")
        {
        }
        public CategoryNotFoundException(string name)
         : base($"Category with name: {name} was not found.")
        {
        }
    }
}
