using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeProject.States
{
    public class ReadyState : IOrderState
    {
        public void Next(OrderContext context)
        {
            Console.WriteLine("[DURUM] Sipariş müşteriye teslim edildi. Afiyet olsun.");
        }

        public void Cancel(OrderContext context)
        {
            Console.WriteLine("[İPTAL REDDEDİLDİ] Kahve zaten hazır ve teslim aşamasında. İptal edilemez!");
        }
    }
}
