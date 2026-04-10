using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CafeProject.API.Data; // Kendi Data namespace'ini kontrol et
using System.Threading.Tasks;

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

        // --- SENİN MEVCUT OLAN METOTLARIN (GetOrders, PlaceOrder vb.) BURADA DURACAK ---
        // (Onları silmene gerek yok, sadece aşağıdaki kısmı ekle)

        // 🚀 DURUM GÜNCELLEME METODU (BARİSTA PANELİNDEKİ BUTONLAR BURAYA VURUR)
        [HttpPut("update-status/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusDto request)
        {
            // Veritabanından o ID'ye ait siparişi buluyoruz
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound("Sipariş veritabanında bulunamadı!");
            }

            // Durumu güncelliyoruz (Örn: "Waiting" -> "Preparing")
            order.Status = request.Status;

            await _context.SaveChangesAsync();

            return Ok("Sipariş durumu başarıyla güncellendi.");
        }
    }

    // JSON Verisini C#'ın anlayacağı formata çeviren DTO Sınıfı
    public class UpdateStatusDto
    {
        public string Status { get; set; }
    }
}