namespace DigitalHouseSystemApi.DTOs
{
    public class BrandDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public BrandDto() { }

        public BrandDto(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
