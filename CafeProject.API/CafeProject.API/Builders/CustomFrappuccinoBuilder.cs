using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeProject.Builders
{
    public class CustomFrappuccinoBuilder : IFrappuccinoBuilder
    {
        private Frappuccino _frappuccino;

        public CustomFrappuccinoBuilder()
        {
            // İnşaata boş bir bardakla başlıyoruz
            _frappuccino = new Frappuccino();

            // Varsayılan değerler atayabiliriz
            _frappuccino.Size = "Orta";
            _frappuccino.MilkType = "Tam Yağlı";
            _frappuccino.Syrup = "Yok";
            _frappuccino.HasIce = false;
            _frappuccino.HasWhippedCream = false;
        }

        public IFrappuccinoBuilder SetSize(string size)
        {
            _frappuccino.Size = size;
            return this;
        }

        public IFrappuccinoBuilder SetMilk(string milkType)
        {
            _frappuccino.MilkType = milkType;
            return this;
        }

        public IFrappuccinoBuilder AddSyrup(string syrup)
        {
            _frappuccino.Syrup = syrup;
            return this;
        }

        public IFrappuccinoBuilder AddIce()
        {
            _frappuccino.HasIce = true;
            return this;
        }

        public IFrappuccinoBuilder AddWhippedCream()
        {
            _frappuccino.HasWhippedCream = true;
            return this;
        }

        public Frappuccino Build()
        {
            // İşi biten kahveyi teslim et
            return _frappuccino;
        }
    }
}
