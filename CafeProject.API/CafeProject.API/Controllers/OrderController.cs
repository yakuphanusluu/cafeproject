using CafeProject.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        // BARİSTA EKRANI İÇİN SİPARİŞLERİ ÇEKER
        [HttpGet("get-orders")]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _context.Orders.ToListAsync();
            return Ok(orders);
        }

        // BARİSTA EKRANI İÇİN DURUM GÜNCELLER
        [HttpPut("update-status/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusDto request)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound("Sipariş bulunamadı.");

            // Gelen durumu (Preparing, Ready vb.) veritabanına yazar
            order.Status = request.Status;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Başarıyla güncellendi", status = request.Status });
        }
    }

    // JS'DEN GELEN JSON'U YAKALAMAK İÇİN ŞART!
    public class UpdateStatusDto
    {
        public string Status { get; set; }
    }
}