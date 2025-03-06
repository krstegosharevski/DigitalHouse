using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.DTOs
{
    public class PrepaidTariffDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int InternetSpeed { get; set; }
        public string ConversationTime { get; set; }
        public string SMS { get; set; }
        public int? RoamingInternet { get; set; }
        public decimal Price { get; set; }

        public PrepaidTariffDto(){}

        public PrepaidTariffDto(int id, string name,
                                int internet_speed, string conversation_time, 
                                string sms, int roaming_internet, decimal price)
        {
            Id = id;
            Name = name;
            InternetSpeed = internet_speed;
            ConversationTime = conversation_time;
            SMS = sms;
            RoamingInternet = roaming_internet;
            Price = price;
        }
    }
}
