namespace DigitalHouseSystemApi.DTOs
{
    public class CategoryDto
    {
        public string Name { get; set; }
        public string? PhotoUrl { get; set; }

        public CategoryDto() { }

        public CategoryDto(string name, string? photoUrl)
        {
            Name = name;
            PhotoUrl = photoUrl;
        }
    }
}
