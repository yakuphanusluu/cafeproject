using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CafeProject.API.Model;
using CafeProject.API.Data;
using System.Threading.Tasks;
using System.Linq;

namespace CafeProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context) { _context = context; }

        [HttpGet("get-orders")]
        public async Task<IActionResult> GetOrders()
        {
            return Ok(await _context.Orders.ToListAsync());
        }

        [HttpPut("update-status/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusDto request)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound("Sipariş bulunamadı.");
            order.Status = request.Status;
            await _context.SaveChangesAsync();
            return Ok(new { message = "Durum güncellendi" });
        }

        [HttpPost("place-order")]
        public async Task<IActionResult> PlaceOrder([FromBody] Order newOrder)
        {
            try
            {
                newOrder.Status = "Waiting";
                newOrder.IsClosed = false;
                _context.Orders.Add(newOrder);

                if (!string.IsNullOrEmpty(newOrder.CustomerUsername))
                {
                    var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Username == newOrder.CustomerUsername);
                    if (customer != null)
                    {
                        int coffeeCount = !string.IsNullOrEmpty(newOrder.CoffeeType) ? newOrder.CoffeeType.Split(" | ").Length : 1;
                        if (newOrder.UsedPoints && customer.LoyaltyPoints >= 10)
                        {
                            customer.LoyaltyPoints -= 10;
                            int earnedPoints = coffeeCount - 1;
                            if (earnedPoints > 0) customer.LoyaltyPoints += earnedPoints;
                        }
                        else { customer.LoyaltyPoints += coffeeCount; }
                    }
                }

                await _context.SaveChangesAsync();
                return Ok(new { message = "Sipariş başarıyla iletildi!" });
            }
            catch (System.Exception ex) { return BadRequest("C# Hatası: " + ex.Message); }
        }

        [HttpPost("take-z-report")]
        public async Task<IActionResult> TakeZReport()
        {
            var openOrders = await _context.Orders.Where(o => o.IsClosed == false).ToListAsync();
            if (openOrders.Count == 0) return BadRequest("Kasa zaten sıfır.");
            foreach (var order in openOrders) order.IsClosed = true;
            await _context.SaveChangesAsync();
            return Ok(new { message = "Gün sonu alındı." });
        }
    }
    public class UpdateStatusDto { public string? Status { get; set; } }
}