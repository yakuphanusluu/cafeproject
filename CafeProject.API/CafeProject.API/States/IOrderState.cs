using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeProject.States
{
    // Tüm durumlar bu şablona uyacak
    public interface IOrderState
    {
        void Next(OrderContext context); // Bir sonraki aşamaya geç
        void Cancel(OrderContext context); // Siparişi iptal etmeye çalış
    }
}
