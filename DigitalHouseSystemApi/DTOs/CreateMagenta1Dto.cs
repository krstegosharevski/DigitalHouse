namespace DigitalHouseSystemApi.DTOs
{
    public class CreateMagenta1Dto
    {
        public int UserId { get; set; }
        public decimal Budget { get; set; }
        public int InternetPackageId { get; set; }
        public ICollection<int> Magenta1TariffsId { get; set; }

        public CreateMagenta1Dto() { }

    }
}
