namespace DigitalHouseSystemApi.Models.Exceptions
{
    public class CantApproveNewMagenta1 : Exception
    {
        public CantApproveNewMagenta1(string name)
        : base($"Magenta1 for user: {name}, can not be approved!")
        {
        }
    }
}
