namespace CafeProject.API.Model
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int OrderId { get; set; }
    }
}