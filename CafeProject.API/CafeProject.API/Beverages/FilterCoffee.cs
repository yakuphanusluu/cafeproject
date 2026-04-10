using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeProject.Interfaces;

namespace CafeProject.Beverages
{
    public class FilterCoffee : IBeverage
    {
        public string GetDescription() => "Filtre Kahve";
        public double GetCost() => 40.0;
    }
}
