using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CafeProject.API.Model;
using CafeProject.API.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CafeProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InventoryController(AppDbContext context) { _context = context; }

        [HttpGet("get")]
        public async Task<IActionResult> GetInventory()
        {
            return Ok(await _context.InventoryItems.ToListAsync());
        }

        // Baristanın toplu stok sayımını veritabanına yazdığı yer
        [HttpPost("update")]
        public async Task<IActionResult> UpdateInventory([FromBody] List<InventoryItem> items)
        {
            foreach (var item in items)
            {
                var existing = await _context.InventoryItems.FirstOrDefaultAsync(i => i.Name == item.Name);
                if (existing != null)
                {
                    existing.Quantity = item.Quantity;
                    existing.LastUpdated = DateTime.Now;
                }
                else
                {
                    item.LastUpdated = DateTime.Now;
                    _context.InventoryItems.Add(item);
                }
            }
            await _context.SaveChangesAsync();
            return Ok(new { message = "Haftalık stok sayımı başarıyla kaydedildi!" });
        }
    }
}