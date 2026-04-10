using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeProject.Interfaces;
using CafeProject.Beverages;

namespace CafeProject.Factories
{
    public class CoffeeFactory : BeverageCreator
    {
        public override IBeverage CreateBeverage(string type)
        {
            return type.ToLower() switch
            {
                "espresso" => new Espresso(),
                "filtre" => new FilterCoffee(),
                _ => throw new ArgumentException("Kafemizde böyle bir kahve bulunmuyor!"),
            };
        }
    }
}
