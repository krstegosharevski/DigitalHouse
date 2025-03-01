namespace DigitalHouseSystemApi.Models
{
    public class TariffType
    {
        public int Id { get; set; }
        public string Name { get; set; } 

        public TariffCategory TariffCategory { get; set; }

        public List<Tariff> Tariffs { get; set; } = new();
    }
}
