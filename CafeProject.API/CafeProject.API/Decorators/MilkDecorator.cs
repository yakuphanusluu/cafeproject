using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeProject.Interfaces;

namespace CafeProject.Decorators
{
    public class MilkDecorator : BeverageDecorator
    {
        public MilkDecorator(IBeverage beverage) : base(beverage) { }

        // Süt eklendiğinde açıklamaya ve fiyata ekleme yapıyoruz
        public override string GetDescription() => base.GetDescription() + ", Süt";
        public override double GetCost() => base.GetCost() + 15.0; // Süt farkı
    }
}
