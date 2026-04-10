using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeProject.Strategies
{
    // Tüm ödeme algoritmaları bu şablona uyacak
    public interface IPaymentStrategy
    {
        void Pay(double amount);
    }
}
