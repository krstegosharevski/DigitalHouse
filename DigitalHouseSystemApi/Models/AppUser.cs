using Microsoft.AspNetCore.Identity;

namespace DigitalHouseSystemApi.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }

        public ICollection<AppUserRole>? UserRoles { get; set; }
    }
}
