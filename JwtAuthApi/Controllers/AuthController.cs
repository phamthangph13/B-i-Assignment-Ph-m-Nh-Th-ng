using System.Linq;
using System.Threading.Tasks;
using JwtAuthApi.Models;
using JwtAuthApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtService _jwtService;

        public AuthController(ApplicationDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            if (userLogin == null || string.IsNullOrEmpty(userLogin.Username) || string.IsNullOrEmpty(userLogin.Password))
            {
                return BadRequest("Invalid client request");
            }

            // Authenticate user
            var user = await _context.Users
                .SingleOrDefaultAsync(u => u.Username == userLogin.Username && u.Password == userLogin.Password);

            if (user == null)
            {
                return Unauthorized("Invalid username or password");
            }

            // Generate JWT token
            var tokenResponse = _jwtService.GenerateToken(user);

            return Ok(tokenResponse);
        }
    }
} 