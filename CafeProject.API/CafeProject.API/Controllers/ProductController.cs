using CafeProject.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

// Namespace kısmını kendi projene göre düzeltmeyi unutma kanka
namespace CafeProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        // JS'in yana döne aradığı (404 veren) o meşhur kapı burası!
        [HttpGet("get-products")]
        public async Task<IActionResult> GetProducts()
        {
            // Veritabanındaki Products (Ürünler) tablosunu listeler
            var products = await _context.Products.ToListAsync();

            return Ok(products);
        }
    }
}