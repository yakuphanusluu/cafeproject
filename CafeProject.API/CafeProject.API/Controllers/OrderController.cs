using Microsoft.AspNetCore.Mvc;
using global::CafeProject.Facades;
using global::CafeProject.API.Data;
using global::CafeProject.API.Models;

namespace CafeProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly OrderFacade _orderFacade;

    public OrderController()
    {
        _orderFacade = new OrderFacade();
    }

    // 1. MÜŞTERİDEN SİPARİŞİ ALAN VE KAYDEDEN KISIM
    [HttpPost("place-order")]
    public IActionResult PlaceOrder([FromBody] OrderRequest request, [FromServices] AppDbContext db)
    {
        var coffee = db.Products.FirstOrDefault(p => p.Name.ToLower() == request.CoffeeType.ToLower());
        var syrup = db.Products.FirstOrDefault(p => p.Name.ToLower() == request.SyrupName.ToLower());

        if (coffee == null) return BadRequest(new { message = "Seçilen kahve menüde bulunamadı!" });

        double total = coffee.Price;
        if (syrup != null) total += syrup.Price;
        if (request.AddMilk) total += db.Products.First(p => p.Name == "Süt").Price;

        var newOrder = new Order
        {
            CoffeeName = coffee.Name,
            SyrupName = syrup != null ? syrup.Name : "Yok",
            HasMilk = request.AddMilk,
            TotalPrice = total,
            OrderDate = DateTime.Now
        };

        db.Orders.Add(newOrder);
        db.SaveChanges();

        return Ok(new
        {
            success = true,
            totalCost = total,
            message = $"Siparişin alındı! Fiş No: #{newOrder.Id} hazırlanıyor."
        });
    }

    // 2. ADMİN EKRANI İÇİN SİPARİŞLERİ GETİREN KISIM
    [HttpGet("get-orders")]
    public IActionResult GetOrders([FromServices] AppDbContext db)
    {
        var pastOrders = db.Orders.OrderByDescending(o => o.OrderDate).ToList();
        return Ok(pastOrders);
    }

    // 3. YENİ! ARAYÜZE DİNAMİK MENÜYÜ GETİREN KISIM
    [HttpGet("get-menu")]
    public IActionResult GetMenu([FromServices] AppDbContext db)
    {
        var products = db.Products.ToList();
        return Ok(products);
    }
}

public class OrderRequest
{
    public string CoffeeType { get; set; } = "";
    public int Amount { get; set; }
    public bool AddMilk { get; set; }
    public string SyrupName { get; set; } = "";
}