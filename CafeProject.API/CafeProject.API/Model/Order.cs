public class Order
{
    public int Id { get; set; }
    public string CoffeeType { get; set; }
    public string SyrupName { get; set; }
    public bool AddMilk { get; set; }
    public bool AddWhip { get; set; } // Yeni
    public string Size { get; set; }    // Yeni
    public string Concept { get; set; } // Yeni
    public string PaymentMethod { get; set; } // Yeni
    public decimal TotalCost { get; set; }
    public string Status { get; set; } = "Waiting"; // Varsayılan: Bekliyor
    public DateTime OrderDate { get; set; } = DateTime.Now;
}