using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeProject.Builders
{
    public interface IFrappuccinoBuilder
    {
        IFrappuccinoBuilder SetSize(string size);
        IFrappuccinoBuilder SetMilk(string milkType);
        IFrappuccinoBuilder AddSyrup(string syrup);
        IFrappuccinoBuilder AddIce();
        IFrappuccinoBuilder AddWhippedCream();
        Frappuccino Build(); // İnşaatı bitir ve ürünü teslim et
    }
}
