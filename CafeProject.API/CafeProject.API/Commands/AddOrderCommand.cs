using CafeProject.API.Data;
using CafeProject.API.Model;

namespace CafeProject.API.Commands
{
    public class AddOrderCommand
    {
        private readonly AppDbContext _context;
        private readonly Order _order;

        // Constructor tam olarak bu iki şeyi almalı:
        public AddOrderCommand(AppDbContext context, Order order)
        {
            _context = context;
            _order = order;
        }

        public void Execute()
        {
            _context.Orders.Add(_order);
            _context.SaveChanges();
        }
    }
}