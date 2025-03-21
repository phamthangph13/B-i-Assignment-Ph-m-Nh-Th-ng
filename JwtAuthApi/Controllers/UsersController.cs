using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtAuthApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Requires authentication for all endpoints in this controller
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            // Omit password from the response
            return await _context.Users
                .Select(u => new User
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                    Role = u.Role
                })
                .ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            // Omit password from the response
            user.Password = null;

            return user;
        }

        // GET: api/Users/profile
        [HttpGet("profile")]
        public async Task<ActionResult<User>> GetProfile()
        {
            // Get username from the authenticated user
            var username = User.Identity?.Name;

            if (string.IsNullOrEmpty(username))
            {
                return BadRequest("User not found");
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                return NotFound();
            }

            // Omit password from the response
            user.Password = null;

            return user;
        }

        // Access control example - only Admin can access this endpoint
        [HttpGet("admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminOnly()
        {
            return Ok(new { message = "Hello Admin! This is a protected endpoint." });
        }
    }
} 