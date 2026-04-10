using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeProject.Iterators
{
    public class CoffeeMenu : IMenu
    {
        private List<MenuItem> _menuItems; // Arkada List kullanıyor

        public CoffeeMenu()
        {
            _menuItems = new List<MenuItem>();
            AddItem("Espresso", 45.0);
            AddItem("Filtre Kahve", 40.0);
            AddItem("Latte", 55.0);
        }

        public void AddItem(string name, double price)
        {
            _menuItems.Add(new MenuItem(name, price));
        }

        public IIterator CreateIterator()
        {
            return new CoffeeIterator(_menuItems);
        }
    }

    // Kahve menüsünün listelerde gezen özel Iterator'ı
    public class CoffeeIterator : IIterator
    {
        private List<MenuItem> _items;
        private int _position = 0;

        public CoffeeIterator(List<MenuItem> items)
        {
            _items = items;
        }

        public bool HasNext() => _position < _items.Count;

        public MenuItem Next()
        {
            MenuItem menuItem = _items[_position];
            _position++;
            return menuItem;
        }
    }
}
