using CafeProject.Builders;      // Builder Pattern
using CafeProject.API.Commands;      // Command Pattern
using CafeProject.API.Data;          // Veritabanı için
using CafeProject.Decorators;    // Decorator Pattern
using CafeProject.API.Facades;       // Facade Pattern
using CafeProject.Factories;     // Factory Pattern
using CafeProject.Iterators;     // Iterator Pattern
using CafeProject.Managers;      // Singleton Pattern (InventoryManager)
using CafeProject.API.Models;        // Order modeli için
using CafeProject.API.Observers;     // Observer Pattern
using CafeProject.States;        // State Pattern
using CafeProject.Strategies;    // Strategy Pattern
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CafeProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context; // [PROXY & REPOSITORY PATTERN]

        public OrderController(AppDbContext context)
        {
            _context = context; // [DEPENDENCY INJECTION PATTERN]
        }

        [HttpPost("place-order")]
        public IActionResult PlaceOrder([FromBody] Order orderDto)
        {
            try
            {
                // 1. [FACADE PATTERN] - Karmaşık süreci tek noktadan yönetir.
                // Not: Eğer metod ismin farklıysa (örneğin CalculatePrice), onu düzeltmen gerekebilir.
                orderDto.TotalCost = OrderFacade.CalculateFinalPrice(orderDto);

                // 2. [STATE PATTERN] - Sipariş 'Waiting' (Bekliyor) durumuyla başlar.
                orderDto.Status = "Waiting";

                // 3. [COMMAND PATTERN] - Siparişi bir komut nesnesiyle kaydediyoruz.
                // Eğer AddOrderCommand parametreleri farklıysa burayı kontrol et.
                var command = new AddOrderCommand(_context, orderDto);
                command.Execute();

                // 4. [OBSERVER PATTERN] - Durum değişikliğini ilgili birimlere haber veriyoruz.
                // OrderStation senin ISubject'in, BaristaScreen ise IObserver'ındır.
                var orderStation = new OrderStation();
                orderStation.Attach(new BaristaScreen());
                orderStation.Notify($"Yeni Sipariş: #{orderDto.Id}");

                return Ok(new
                {
                    success = true,
                    orderId = orderDto.Id,
                    totalCost = orderDto.TotalCost
                });
            }
            catch (Exception ex)
            {
                // Hata durumunda ne olduğunu konsolda görelim
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("get-orders")]
        public IActionResult GetOrders()
        {
            // [ITERATOR PATTERN] - EF Core listeleri bu deseni temel alır.
            var orders = _context.Orders.OrderByDescending(o => o.Id).ToList();
            return Ok(orders);
        }

        [HttpGet("get-order-status/{id}")]
        public IActionResult GetStatus(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null) return NotFound();
            return Ok(new { status = order.Status });
        }

        [HttpPost("update-status")]
        public IActionResult UpdateStatus([FromBody] StatusUpdateDto dto)
        {
            var order = _context.Orders.Find(dto.Id);
            if (order == null) return NotFound();

            // [STATE PATTERN] - Durum burada güncellenir.
            order.Status = dto.Status;
            _context.SaveChanges();

            return Ok(new { success = true });
        }
    }

    public class StatusUpdateDto { public int Id { get; set; } public string Status { get; set; } }
}