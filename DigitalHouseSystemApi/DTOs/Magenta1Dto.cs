using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.DTOs
{
    public class Magenta1Dto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public decimal Budget { get; set; }
        public string InternetPackage { get; set; }
        public ICollection<string> Magenta1Tariffs { get; set; }

        public Magenta1Dto() { }

        public Magenta1Dto(int id,string username,  decimal budget, string internetPackage, ICollection<string> magenta1tariffs)
        {
            Id = id;
            Username = username;
            Budget = budget;
            InternetPackage = internetPackage;
            Magenta1Tariffs = magenta1tariffs;
        }


    }
}
