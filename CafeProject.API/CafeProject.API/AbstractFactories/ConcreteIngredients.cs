using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeProject.AbstractFactories
{
    // --- STANDART MALZEMELER ---
    public class CowMilk : IMilk
    {
        public string GetMilkInfo() => "Tam Yağlı İnek Sütü";
    }

    public class StandardSyrup : ISyrupIngredient
    {
        public string GetSyrupInfo() => "Standart Şurup (Yapay Aroma İçerir)";
    }

    // --- VEGAN MALZEMELER ---
    public class OatMilk : IMilk
    {
        public string GetMilkInfo() => "Organik Yulaf Sütü (Vegan)";
    }

    public class VeganSyrup : ISyrupIngredient
    {
        public string GetSyrupInfo() => "Doğal Özütlü Agave Şurubu (Vegan)";
    }
}
