namespace DigitalHouseSystemApi.DTOs
{
    public class ColorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HexCode { get; set; }

        public ColorDto() { }

        public ColorDto(int id, string name, string hexCode)
        {
            Id = id;
            Name = name;
            HexCode = hexCode;
        }
    }
}
