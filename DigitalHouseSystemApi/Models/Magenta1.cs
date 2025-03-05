using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalHouseSystemApi.Models
{
    public class Magenta1
    {
        [Key, ForeignKey("AppUser")]
        public int Id { get; set; }
        public AppUser AppUser { get; set; }

        public decimal Budget { get; set; }

        public int InternetPackageId { get; set; }
        public InternetPackage InternetPackage { get; set; }

        public ICollection<Magenta1Tariff> Magenta1Tariffs { get; set; }

        public void AddTariff(Tariff tariff)
        {
            if (tariff.TariffType.Name != "Magenta1")
            {
                throw new InvalidOperationException("Only tariffs with TariffType 'Magenta1' can be added.");
            }

            if (Magenta1Tariffs.Count >= 5)
            {
                throw new InvalidOperationException("A Magenta1 package can have a maximum of 5 tariffs.");
            }

            Magenta1Tariffs.Add(new Magenta1Tariff { Tariff = tariff, Magenta1 = this });
        }

    }
}
