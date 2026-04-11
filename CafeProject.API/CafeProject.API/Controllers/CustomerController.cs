using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CafeProject.API.Model;
using CafeProject.API.Data;

namespace CafeProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomerController(AppDbContext context) { _context = context; }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Customer dto)
        {
            if (await _context.Customers.AnyAsync(c => c.Username == dto.Username))
                return BadRequest("Bu kullanıcı adı zaten alınmış!");

            dto.LoyaltyPoints = 0;
            _context.Customers.Add(dto);
            await _context.SaveChangesAsync();
            return Ok(dto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Customer dto)
        {
            var user = await _context.Customers.FirstOrDefaultAsync(c => c.Username == dto.Username && c.Password == dto.Password);
            if (user == null) return Unauthorized("Kullanıcı adı veya şifre hatalı!");
            return Ok(user);
        }

        [HttpGet("get-points/{username}")]
        public async Task<IActionResult> GetPoints(string username)
        {
            var user = await _context.Customers.FirstOrDefaultAsync(c => c.Username == username);
            if (user == null) return NotFound();
            return Ok(user);
        }
    }
}