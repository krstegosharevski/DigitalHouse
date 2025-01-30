namespace DigitalHouseSystemApi.Models.Exceptions
{
    public class SearchNotFoundException : Exception
    {
        public SearchNotFoundException(string search)
          : base($"There is no product with name {search}.")
        {
        }
    }
}
