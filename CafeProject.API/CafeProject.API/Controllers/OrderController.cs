using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CafeProject.API.Model;
using CafeProject.API.Data;

namespace CafeProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;
        public OrderController(AppDbContext context) { _context = context; }

        [HttpGet("get-orders")]
        public async Task<IActionResult> GetOrders() { return Ok(await _context.Orders.ToListAsync()); }

        [HttpPut("update-status/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusDto request)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();
            order.Status = request.Status;
            await _context.SaveChangesAsync();
            return Ok(new { message = "Status Updated", status = order.Status });
        }

        [HttpPost("place-order")]
        public async Task<IActionResult> PlaceOrder([FromBody] Order newOrder)
        {
            newOrder.Status = "Waiting";
            _context.Orders.Add(newOrder);
            if (!string.IsNullOrEmpty(newOrder.CustomerUsername))
            {
                var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Username == newOrder.CustomerUsername);
                if (customer != null)
                {
                    int coffeeCount = newOrder.CoffeeType?.Split(" | ").Length ?? 1;
                    if (newOrder.UsedPoints && customer.LoyaltyPoints >= 10)
                    {
                        customer.LoyaltyPoints -= 10;
                        int earned = coffeeCount - 1;
                        if (earned > 0) customer.LoyaltyPoints += earned;
                    }
                    else { customer.LoyaltyPoints += coffeeCount; }
                }
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("take-z-report")]
        public async Task<IActionResult> TakeZReport()
        {
            var open = await _context.Orders.Where(o => !o.IsClosed).ToListAsync();
            foreach (var o in open) o.IsClosed = true;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
    public class UpdateStatusDto { public string Status { get; set; } = ""; }
}