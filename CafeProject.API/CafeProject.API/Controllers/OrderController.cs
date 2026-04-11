using CafeProject.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
// Kendi projendeki Data ve Models klasörlerinin namespace'lerini buraya ekle (Örnek: using CafeProject.API.Data;)

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

        // 🟢 1. MÜŞTERİNİN SİPARİŞ VERDİĞİ METOT
        [HttpPost("place-order")]
        public async Task<IActionResult> PlaceOrder([FromBody] Order request)
        {
            if (request == null)
            {
                return BadRequest("Sipariş verisi boş geldi.");
            }

            // Sisteme düşen ilk sipariş otomatik olarak 'Waiting' (Bekliyor) durumunda başlar
            request.Status = "Waiting";

            _context.Orders.Add(request);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Sipariş başarıyla alındı!", orderId = request.Id });
        }

        // 🟢 2. BARİSTA VE CANLI TAKİP EKRANININ SİPARİŞLERİ ÇEKTİĞİ METOT
        [HttpGet("get-orders")]
        public async Task<IActionResult> GetOrders()
        {
            // Siparişleri, altındaki 'Items' (sepetteki kahveler) ile birlikte getirir
            var orders = await _context.Orders
                                       .Include(o => o.Items)
                                       .ToListAsync();

            return Ok(orders);
        }

        // 🟢 3. BARİSTANIN DURUMU GÜNCELLEDİĞİ METOT (Preparing, Ready, Completed)
        [HttpPut("update-status/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusDto request)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound("Sipariş veritabanında bulunamadı!");
            }

            // Durumu güncelliyoruz
            order.Status = request.Status;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Sipariş durumu başarıyla güncellendi.", newStatus = request.Status });
        }
    }

    // JSON'dan gelen { "status": "Ready" } bilgisini C#'ın anlayacağı formata çeviren yardımcı sınıf
    public class UpdateStatusDto
    {
        public string Status { get; set; }
    }
}