namespace DigitalHouseSystemApi.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? PhotoUrl { get; set; }

        public CategoryDto() { }

        public CategoryDto(int id, string name, string? photoUrl)
        {
            Id = id;
            Name = name;
            PhotoUrl = photoUrl;
        }
    }
}
