using Microsoft.AspNetCore.Identity;

namespace DigitalHouseSystemApi.Models
{
    public class AppRole : IdentityRole<int>
    {
        public ICollection<AppUserRole>? UserRoles { get; set; }
    }
}
