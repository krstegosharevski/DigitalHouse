namespace DigitalHouseSystemApi.DTOs
{
    public class Trust12TariffDto
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

        public Trust12TariffDto() {}

        public Trust12TariffDto(int id, string name,
                                int internet_speed, string conversation_time,
                                string sms, int roaming_internet, decimal price,
                                int international_network_calls, bool e_bill,
                                decimal discount)
        {
            Id = id;
            Name = name;
            InternetSpeed = internet_speed;
            ConversationTime = conversation_time;
            SMS = sms;
            RoamingInternet = roaming_internet;
            InternationalNetworkCalls = international_network_calls;
            E_bill = e_bill;
            Price = price;
            Discount = discount;
        }
    }
}
