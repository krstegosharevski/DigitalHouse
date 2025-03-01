namespace DigitalHouseSystemApi.Models
{
    public class InternetPackage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int InternetSpeed { get; set; }
        public string ConversationTime { get; set; }
        public int MagentaTV { get; set; }
        public bool MagentaTV_GO { get; set; }
        public string? AdvancedFunctions { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public List<Magenta1> Magentas { get; set; } = new();
    }
}
