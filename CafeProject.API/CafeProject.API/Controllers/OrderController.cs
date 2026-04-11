using CafeProject.API.Model; // Kendi Model klasörünün yoluna göre ayarla
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using CafeProject.API.Data; // Kendi Data klasörünün yoluna göre ayarla

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

        // 1. KAPI: Siparişleri çekme (Barista ve Patron ekranı için)
        [HttpGet("get-orders")]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _context.Orders.ToListAsync();
            return Ok(orders);
        }

        // 2. KAPI: Durum güncelleme (Barista ekranı için)
        [HttpPut("update-status/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusDto request)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound("Sipariş bulunamadı.");

            order.Status = request.Status;
            await _context.SaveChangesAsync();
            return Ok(new { message = "Durum güncellendi", status = request.Status });
        }

        // 3. KAPI: Yeni sipariş verme (Müşteri ekranı için)
        [HttpPost("place-order")]
        public async Task<IActionResult> PlaceOrder([FromBody] Order newOrder)
        {
            try
            {
                newOrder.Status = "Waiting";
                newOrder.IsClosed = false; // Yeni sipariş kasaya işlenmek üzere açık gelir

                _context.Orders.Add(newOrder);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Sipariş başarıyla iletildi!" });
            }
            catch (System.Exception ex)
            {
                return BadRequest("C# Hatası: " + ex.Message);
            }
        }

        // 4. KAPI (YENİ): Patronun Gün Sonu (Z Raporu) aldığı yer
        [HttpPost("take-z-report")]
        public async Task<IActionResult> TakeZReport()
        {
            // Henüz kapatılmamış siparişleri bul
            var openOrders = await _context.Orders.Where(o => o.IsClosed == false).ToListAsync();

            if (openOrders.Count == 0) return BadRequest("Kasa zaten sıfır, kapatılacak sipariş yok.");

            // Hepsini "Kapatıldı" olarak işaretle
            foreach (var order in openOrders)
            {
                order.IsClosed = true;
            }

            await _context.SaveChangesAsync();
            return Ok(new { message = "Gün sonu başarıyla alındı, kasa sıfırlandı!" });
        }
    }

    public class UpdateStatusDto
    {
        public string? Status { get; set; }
    }
}