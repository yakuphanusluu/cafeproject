namespace CafeProject.API.Models;

public class Order
{
    public int Id { get; set; }
    public string CoffeeName { get; set; } = "";
    public string SyrupName { get; set; } = "";
    public bool HasMilk { get; set; }
    public double TotalPrice { get; set; }
    public DateTime OrderDate { get; set; } // Siparişin verildiği tarih ve saat
}