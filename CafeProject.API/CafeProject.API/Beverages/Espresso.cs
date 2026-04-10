using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeProject.Interfaces;

namespace CafeProject.Beverages
{
    public class Espresso : IBeverage
    {
        public string GetDescription() => "Espresso";
        public double GetCost() => 45.0;
    }
}
