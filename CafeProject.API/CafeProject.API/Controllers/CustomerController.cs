using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CafeProject.API.Model;
using CafeProject.API.Data; // AppDbContext'in olduğu yer

namespace CafeProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomerController(AppDbContext context)
        {
            _context = context;
        }

        // Telefondan puan sorgula veya yeni kayıt aç
        [HttpGet("get-or-create/{phone}")]
        public async Task<IActionResult> GetOrCreateCustomer(string phone)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.PhoneNumber == phone);

            if (customer == null)
            {
                customer = new Customer { PhoneNumber = phone, Name = "Yeni Müdavim", LoyaltyPoints = 0 };
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
            }

            return Ok(customer);
        }
    }
}