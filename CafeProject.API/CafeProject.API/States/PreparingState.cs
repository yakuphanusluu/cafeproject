using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CafeProject.States
{
    public class PreparingState : IOrderState
    {
        public void Next(OrderContext context)
        {
            Console.WriteLine("[DURUM] Barista kahveyi bitirdi. Sipariş hazır!");
            context.SetState(new ReadyState()); // Durum güncellendi!
        }

        public void Cancel(OrderContext context)
        {
            Console.WriteLine("[İPTAL REDDEDİLDİ] Barista şu an kahveyi hazırlıyor, malzemeler kullanıldı. İptal edilemez!");
        }
    }
}
