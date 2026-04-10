using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeProject.Commands
{
    public class AddOrderCommand : ICommand
    {
        private Ticket _ticket;
        private string _item;

        // Komut çalıştırıldığında hangi adisyona hangi ürünün ekleneceğini biliyor
        public AddOrderCommand(Ticket ticket, string item)
        {
            _ticket = ticket;
            _item = item;
        }

        public void Execute()
        {
            _ticket.AddOrder(_item);
        }

        public void Undo()
        {
            _ticket.RemoveOrder(_item);
        }
    }
}
