using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Controllers
{
    public class AdminController : BaseApiController
    {

        private readonly UserManager<AppUser> _userManager;
        public AdminController( UserManager<AppUser> userManager) 
        { 
           _userManager = userManager;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("users-with-roles")]
        public async Task<ActionResult> GetUsersWithRoles()
        {
            var users = await _userManager.Users
                .OrderBy(u => u.UserName)
                .Select(u => new
                {
                    u.Id,
                    Username = u.UserName,
                    Roles = u.UserRoles.Select(r => r.Role.Name).ToList()
                }).ToListAsync();

            return Ok(users);
                
        }

    }
}
