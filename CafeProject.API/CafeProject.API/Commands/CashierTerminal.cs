using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeProject.Commands
{
    public class CashierTerminal
    {
        // Yapılan işlemleri hafızada tutmak için Stack (Yığın) kullanıyoruz (LIFO - Son giren ilk çıkar)
        private Stack<ICommand> _commandHistory = new Stack<ICommand>();

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            _commandHistory.Push(command); // İşlemi hafızaya kaydet
        }

        public void UndoLastOrder()
        {
            if (_commandHistory.Count > 0)
            {
                Console.WriteLine("[TERMİNAL] Müşteri vazgeçti. Son işlem geri alınıyor...");
                ICommand lastCommand = _commandHistory.Pop(); // Son komutu hafızadan çıkar
                lastCommand.Undo(); // Geri al!
            }
            else
            {
                Console.WriteLine("[TERMİNAL] Geri alınacak işlem yok!");
            }
        }
    }
}
