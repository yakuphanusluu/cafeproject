using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeProject.Adapters
{
    // Target: Bizim sistemimizin dış siparişleri almak için beklediği standart
    public interface IDeliveryAdapter
    {
        void ProcessDeliveryOrder();
    }
}
