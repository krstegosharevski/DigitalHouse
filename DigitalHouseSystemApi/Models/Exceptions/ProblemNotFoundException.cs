namespace DigitalHouseSystemApi.Models.Exceptions
{
    public class ProblemNotFoundException : Exception
    {
        public ProblemNotFoundException(int id)
          : base($"Problem post with Id: {id} was not found.")
        {
        }
    }
}
