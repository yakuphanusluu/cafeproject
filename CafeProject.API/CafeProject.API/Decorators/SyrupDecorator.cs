using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeProject.Interfaces;

namespace CafeProject.Decorators
{
    public class SyrupDecorator : BeverageDecorator
    {
        private string _syrupName;
        private double _syrupCost;

        public SyrupDecorator(IBeverage beverage, string syrupName, double syrupCost) : base(beverage)
        {
            _syrupName = syrupName;
            _syrupCost = syrupCost;
        }

        public override string GetDescription() => base.GetDescription() + $", {_syrupName} Şurubu";
        public override double GetCost() => base.GetCost() + _syrupCost;
    }
}
