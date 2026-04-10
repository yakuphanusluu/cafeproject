namespace CafeProject.API.Facades
{
    using CafeProject.API.Models;
    public class OrderFacade
    {
        // Metod isminin ve 'static' olduğunun doğruluğundan emin ol
        public static decimal CalculateFinalPrice(Order order)
        {
            // Burada basit bir hesaplama dönelim, kırmızıyı söndürsün
            decimal total = 40;
            if (order.Size == "Large") total += 20;
            if (order.AddMilk) total += 10;
            return total;
        }
    }
}