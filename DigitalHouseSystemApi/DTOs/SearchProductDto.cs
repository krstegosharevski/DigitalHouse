namespace DigitalHouseSystemApi.DTOs
{
    public class SearchProductDto
    {
        public string Name { get; set; }
        public string? PhotoUrl { get; set; }

        public SearchProductDto() { }

        public SearchProductDto(string name, string photoUrl)
        {
            Name = name;
            PhotoUrl = photoUrl;
        }
    }
}
