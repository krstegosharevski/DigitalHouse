namespace DigitalHouseSystemApi.DTOs
{
    public class ColorDto
    {
        public string Name { get; set; }
        public string HexCode { get; set; }

        public ColorDto() { }

        public ColorDto(string name, string hexCode)
        {
            Name = name;
            HexCode = hexCode;
        }
    }
}
