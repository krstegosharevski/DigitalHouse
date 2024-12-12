using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DigitalHouseSystemApi.Data;
using DigitalHouseSystemApi.Models;
using DigitalHouseSystemApi.Interfaces;
using DigitalHouseSystemApi.DTOs;

namespace DigitalHouseSystemApi.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;

        public UsersController (ApplicationDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

      

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users = await _userRepository.GetUsersAsync();
            
            if(users == null || !users.Any())
            {
                return NotFound("Users not found");
            }

            return Ok(users);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
        }


    }
}
