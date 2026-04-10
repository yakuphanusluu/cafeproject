using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeProject.Interfaces;

namespace CafeProject.Factories
{
    public abstract class BeverageCreator
    {
        public abstract IBeverage CreateBeverage(string type);
    }
}
