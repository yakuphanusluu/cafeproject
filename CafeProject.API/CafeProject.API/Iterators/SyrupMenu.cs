using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeProject.Iterators
{
    public class SyrupMenu : IMenu
    {
        private MenuItem[] _menuItems; // Arkada Array kullanıyor
        private int _position = 0;

        public SyrupMenu()
        {
            _menuItems = new MenuItem[3]; // Şimdilik 3 şurup
            AddItem("Çilek Şurubu", 15.0);
            AddItem("Fıstık Şurubu", 18.0);
            AddItem("Karamel Şurubu", 12.0);
        }

        public void AddItem(string name, double price)
        {
            if (_position < _menuItems.Length)
            {
                _menuItems[_position] = new MenuItem(name, price);
                _position++;
            }
        }

        public IIterator CreateIterator()
        {
            return new SyrupIterator(_menuItems);
        }
    }

    // Şurup menüsünün dizilerde gezen özel Iterator'ı
    public class SyrupIterator : IIterator
    {
        private MenuItem[] _items;
        private int _position = 0;

        public SyrupIterator(MenuItem[] items)
        {
            _items = items;
        }

        public bool HasNext() => _position < _items.Length && _items[_position] != null;

        public MenuItem Next()
        {
            MenuItem menuItem = _items[_position];
            _position++;
            return menuItem;
        }
    }
}
