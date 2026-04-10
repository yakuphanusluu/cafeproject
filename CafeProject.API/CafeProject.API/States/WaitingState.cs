using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeProject.States
{
    public class WaitingState : IOrderState
    {
        public void Next(OrderContext context)
        {
            Console.WriteLine("[DURUM] Sipariş alındı, hazırlanma aşamasına geçiliyor...");
            context.SetState(new PreparingState()); // Durum güncellendi!
        }

        public void Cancel(OrderContext context)
        {
            Console.WriteLine("[İPTAL] Sipariş henüz mutfağa iletilmemişti. İptal işlemi başarılı.");
            // Burada durum iptal edildi state'ine de çekilebilir
        }
    }
}
