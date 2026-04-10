using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeProject.Interfaces
{
    public interface IBeverage
    {
        string GetDescription();
        double GetCost();
    }
}
