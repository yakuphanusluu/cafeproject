using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeProject.Commands
{
    // Her komutun bir 'Çalıştır' ve bir 'Geri Al' metodu olmak zorundadır.
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}
