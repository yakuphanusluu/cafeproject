using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeProject.Managers
{
    public class InventoryManager
    {
        // 1. Kendi örneğini statik ve private olarak tutuyoruz. Dışarıdan görünmez.
        private static InventoryManager _instance;

        // 2. Çoklu thread (iş parçacığı) güvenliği için kilit nesnesi.
        // İki sipariş aynı anda gelirse stoğun şaşmamasını sağlar.
        private static readonly object _lock = new object();

        // 3. EN ÖNEMLİ ADIM: Constructor (Yapıcı Metot) 'private' yapıldı!
        // Artık dışarıdan kimse 'new InventoryManager()' diyemez.
        private InventoryManager()
        {
            // Kafenin sabah açılışındaki başlangıç stokları (gram/mililitre bazında)
            CoffeeBeans = 1000;
            Milk = 5000;
        }

        // 4. Nesneye ulaşmak için tek açık kapı (Global Access Point)
        public static InventoryManager Instance
        {
            get
            {
                // Çift kontrollü kilit mekanizması (Double-checked locking)
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new InventoryManager();
                        }
                    }
                }
                return _instance;
            }
        }

        // --- STOK İŞLEMLERİ (Singleton'ın yapacağı işler) ---

        public int CoffeeBeans { get; private set; }
        public int Milk { get; private set; }

        // Kahve hazırlarken stoktan düşmek için kullanılacak metot
        public bool UseCoffee(int amount)
        {
            if (CoffeeBeans >= amount)
            {
                CoffeeBeans -= amount;
                return true;
            }
            Console.WriteLine("Uyarı: Yeterli kahve çekirdeği yok!");
            return false;
        }
    }
}
