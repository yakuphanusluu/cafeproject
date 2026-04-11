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

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("get-orders")]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _context.Orders.ToListAsync();
            return Ok(orders);
        }

        [HttpPut("update-status/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusDto request)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound("Sipariş bulunamadı.");

            order.Status = request.Status;
            await _context.SaveChangesAsync();
            return Ok(new { message = "Durum güncellendi", status = request.Status });
        }

        [HttpPost("place-order")]
        public async Task<IActionResult> PlaceOrder([FromBody] Order newOrder)
        {
            try
            {
                newOrder.Status = "Waiting";
                newOrder.IsClosed = false;

                _context.Orders.Add(newOrder);

                // YENİ: Müşteri numarası varsa puanını artır
                if (!string.IsNullOrEmpty(newOrder.CustomerPhone))
                {
                    var customer = await _context.Customers.FirstOrDefaultAsync(c => c.PhoneNumber == newOrder.CustomerPhone);
                    if (customer != null)
                    {
                        customer.LoyaltyPoints += 1; // Her siparişte 1 puan
                    }
                    else
                    {
                        // İlk siparişi veren yepyeni biriyse direkt kaydet ve 1 puan ver
                        var newCustomer = new Customer
                        {
                            PhoneNumber = newOrder.CustomerPhone,
                            Name = newOrder.CustomerName ?? "Yeni Müdavim",
                            LoyaltyPoints = 1
                        };
                        _context.Customers.Add(newCustomer);
                    }
                }

                await _context.SaveChangesAsync();
                return Ok(new { message = "Sipariş başarıyla iletildi!" });
            }
            catch (System.Exception ex)
            {
                return BadRequest("C# Hatası: " + ex.Message);
            }
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