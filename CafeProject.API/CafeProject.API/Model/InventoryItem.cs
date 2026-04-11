using System;
namespace CafeProject.API.Model
{
    public class InventoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }
}