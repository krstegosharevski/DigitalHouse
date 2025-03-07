using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.DTOs
{
    public class InternetPackageDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string InternetSpeed { get; set; }
        public string ConversationTime { get; set; }
        public int MagentaTV { get; set; }
        public bool MagentaTV_GO { get; set; }
        public string Functions { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }

        public InternetPackageDto()
        {
            
        }

        public InternetPackageDto(int id, string name, string internet_speed,
                                    string conversation_time, int magenta_tv,
                                    bool magenta_go, string functions,
                                    decimal price, decimal discount)
        {
            Id = id;
            Name = name;
            InternetSpeed = internet_speed;
            ConversationTime = conversation_time;
            MagentaTV = magenta_tv;
            MagentaTV_GO = magenta_go;
            Functions = functions;
            Price = price;
            Discount = discount;
        }
    }
}
