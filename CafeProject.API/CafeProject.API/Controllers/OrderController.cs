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

                // 1. MÜDAVİM PUAN HESAPLAMASI
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
                        else
                        {
                            customer.LoyaltyPoints += coffeeCount;
                        }
                    }
                }

                // 2. OTOMATİK STOK DÜŞME SİSTEMİ
                if (newOrder.Items != null && newOrder.Items.Count > 0)
                {
                    foreach (var item in newOrder.Items)
                    {
                        // id'si 999 olan Mixology İksiri hariç (çünkü onun sabit bir stoku yok)
                        if (item.ProductId > 0 && item.ProductId != 999)
                        {
                            var productInDb = await _context.Products.FindAsync(item.ProductId);
                            if (productInDb != null && productInDb.Stock > 0)
                            {
                                productInDb.Stock -= 1; // Sepetteki her ürün için stoku 1 düşür
                            }
                        }
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