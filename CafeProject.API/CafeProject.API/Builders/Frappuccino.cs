using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeProject.Builders
{
    public class Frappuccino
    {
        public string Size { get; set; }
        public string MilkType { get; set; }
        public string Syrup { get; set; }
        public bool HasIce { get; set; }
        public bool HasWhippedCream { get; set; }

        public void ShowDetails()
        {
            Console.WriteLine($"\n[ÖZEL ÜRETİM FRAPPUCCINO]");
            Console.WriteLine($"- Boyut: {Size}");
            Console.WriteLine($"- Süt: {MilkType}");
            Console.WriteLine($"- Şurup: {Syrup}");
            Console.WriteLine($"- Buz: {(HasIce ? "Var" : "Yok")}");
            Console.WriteLine($"- Krem Şanti: {(HasWhippedCream ? "Var" : "Yok")}");
            Console.WriteLine("--------------------------\n");
        }
    }
}
