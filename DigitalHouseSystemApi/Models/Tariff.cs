namespace DigitalHouseSystemApi.Models
{
    public class Tariff
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int InternetSpeed { get; set; }
        public string ConversationTime { get; set; }
        public string SMS { get; set; }  
        public int? RoamingInternet { get; set; }
        public int? InternationalNetworkCalls { get; set; }
        public bool E_bill { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }


        public ICollection<Magenta1Tariff>? Magenta1Tariffs { get; set; }

        public int TariffTypeId { get; set; }
        public TariffType TariffType { get; set; }
    }
}
