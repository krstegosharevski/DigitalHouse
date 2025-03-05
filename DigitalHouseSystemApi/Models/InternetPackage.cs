namespace DigitalHouseSystemApi.Models
{
    public class InternetPackage
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
        public List<Magenta1>? Magenta1s { get; set; } = new();
    }
}
