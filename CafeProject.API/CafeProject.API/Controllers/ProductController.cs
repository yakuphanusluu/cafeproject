using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CafeProject.API.Model;
using CafeProject.API.Data;
using System.Threading.Tasks;

namespace CafeProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context) { _context = context; }

        [HttpGet("get-products")]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _context.Products.ToListAsync());
        }

        [HttpPut("update-stock/{id}")]
        public async Task<IActionResult> UpdateStock(int id, [FromBody] StockUpdateDto dto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound("Ürün bulunamadı.");

            product.Stock = dto.Stock;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Stok başarıyla güncellendi!" });
        }
    }

    public class StockUpdateDto
    {
        public int Stock { get; set; }
    }
}