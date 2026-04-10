using System;
using CafeProject.Factories;
using CafeProject.Interfaces;
using CafeProject.Managers;
using CafeProject.Decorators;
using CafeProject.Strategies;

namespace CafeProject.Facades
{
    public class OrderFacade
    {
        // Facade, arka plandaki karmaşık sistemleri tanır
        private BeverageCreator _factory;
        private InventoryManager _inventory;

        public OrderFacade()
        {
            _factory = new CoffeeFactory();
            _inventory = InventoryManager.Instance;
        }

        // Kasiyerin kullanacağı TEK VE BASİT metot
        public double PlaceOrder(string coffeeType, int coffeeAmount, bool addMilk, string syrupName)
        {
            Console.WriteLine($"\n--- YENİ SİPARİŞ: {coffeeType.ToUpper()} ---");

            // 1. Adım: Stok Kontrolü (Kasiyer bunu düşünmek zorunda kalmıyor)
            if (!_inventory.UseCoffee(coffeeAmount))
            {
                Console.WriteLine("Sipariş iptal edildi: Yetersiz kahve çekirdeği stoğu.");
                return 0;
            }

            // 2. Adım: Temel Kahveyi Üret
            IBeverage coffee = _factory.CreateBeverage(coffeeType);

            // 3. Adım: İsteğe Bağlı Süslemeleri Ekle
            if (addMilk)
            {
                coffee = new MilkDecorator(coffee);
            }

            if (!string.IsNullOrEmpty(syrupName))
            {
                // Kolaylık olsun diye şurup fiyatını sabit 15 TL aldık
                coffee = new SyrupDecorator(coffee, syrupName, 15.0);
            }

            Console.WriteLine($"Sipariş Hazır: {coffee.GetDescription()}");
            Console.WriteLine($"Hesap: {coffee.GetCost()} TL");
            Console.WriteLine("----------------------------------");

            return coffee.GetCost(); // Fiyatı geri döndürüyoruz
        }

        // Ödeme işlemini dışarıdan gelen stratejiye (algoritmaya) göre yapar
        public void PayOrder(double amount, IPaymentStrategy paymentMethod)
        {
            if (amount > 0)
            {
                // Ödeme stratejisi (nakit veya kart) burada çalıştırılır
                paymentMethod.Pay(amount);
                Console.WriteLine("Fişiniz yazdırılıyor. Afiyet olsun!\n");
            }
        }
    }
}