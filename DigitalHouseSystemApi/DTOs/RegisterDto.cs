using System.ComponentModel.DataAnnotations;

namespace DigitalHouseSystemApi.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
        
        public string? Name { get; set; }
        public string? LastName { get; set; }
    }
}
