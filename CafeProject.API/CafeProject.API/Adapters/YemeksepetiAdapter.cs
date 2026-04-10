using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeProject.Facades;

namespace CafeProject.Adapters
{
    // Adapter: Dış sistemi bizim sisteme bağlayan çevirici
    public class YemeksepetiAdapter : IDeliveryAdapter
    {
        private YemeksepetiService _externalService;
        private OrderFacade _facade;

        public YemeksepetiAdapter(OrderFacade facade)
        {
            _externalService = new YemeksepetiService();
            _facade = facade; // Kendi sistemimizin ön yüzünü içeri alıyoruz
        }

        public void ProcessDeliveryOrder()
        {
            // 1. Dışarıdan veriyi al
            string rawData = _externalService.FetchOrderFromApi();

            // 2. Bizim sistemin anlayacağı şekilde parçala (Parse)
            string[] parts = rawData.Split('-');
            string type = parts[0];
            int amount = int.Parse(parts[1]);
            bool hasMilk = parts[2] == "sutlu";
            string syrup = parts[3] == "yok" ? "" : parts[3];

            Console.WriteLine("\n[ADAPTER AKTİF] Yemeksepeti API'den sipariş geldi, sisteme çevriliyor...");

            // 3. Kendi sistemimize (Facade) gönder
            _facade.PlaceOrder(type, amount, hasMilk, syrup);
        }
    }
}
