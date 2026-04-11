using CafeProject.API.Data; // Kendi Model klasörünün yoluna göre gerekirse burayı ayarla
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CafeProject.API.Model; // Kendi Model klasörünün yoluna göre gerekirse burayı ayarla

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

        // 1. KAPI: Baristanın siparişleri çektiği yer
        [HttpGet("get-orders")]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _context.Orders.ToListAsync();
            return Ok(orders);
        }

        // 2. KAPI: Baristanın "Hazırlanıyor", "Hazır" yaptığı yer
        [HttpPut("update-status/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusDto request)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound("Sipariş bulunamadı.");

            order.Status = request.Status;
            await _context.SaveChangesAsync();
            return Ok(new { message = "Durum güncellendi", status = request.Status });
        }

        // 3. KAPI (EKSİK OLAN BUYDU): Müşterinin sipariş gönderdiği yer
        [HttpPost("place-order")]
        public async Task<IActionResult> PlaceOrder([FromBody] Order newOrder)
        {
            try
            {
                // Yeni gelen siparişi her ihtimale karşı "Waiting" (Bekliyor) yapıyoruz
                newOrder.Status = "Waiting";

                // Siparişi veritabanına ekle ve kaydet
                _context.Orders.Add(newOrder);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Sipariş başarıyla baristaya iletildi!" });
            }
            catch (System.Exception ex)
            {
                // Eğer veritabanı veya modelde bir uyumsuzluk varsa hatanın detayını fırlatır
                return BadRequest("C# Hatası: " + ex.Message);
            }
        }
    }

    // Durum güncellemesi için gereken paket
    public class UpdateStatusDto
    {
        public string? Status { get; set; }
    }
}