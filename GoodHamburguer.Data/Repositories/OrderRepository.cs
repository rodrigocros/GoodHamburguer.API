using GoodHamburguer.Business.Models;
using GoodHamburguer.Business.Services.Interfaces;
using GoodHamburguer.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburguer.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }
        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public Order CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order;
        }

        public bool DeleteOrder(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null)
            {
                return false;
            }
            _context.Orders.Remove(order);
            _context.SaveChanges();
            return true;
        }
        public Order GetOrderById(int id)
        {
            return _context.Orders.Find(id);
        }

        public List<Order> GetOrders()
        {
            return _context.Orders.ToList();

        }

        public Order UpdateOrder(int id, Order updatedOrder)
        {
            var order = _context.Orders.Find(id);
            if (order == null)
            {
                return null;
            }
            order.SoftDrinkId = updatedOrder.SoftDrinkId;
            order.FriesId = updatedOrder.FriesId;
            order.SandwichId = updatedOrder.SandwichId;
            order.Total = updatedOrder.Total;
            _context.Orders.Update(order);
            _context.SaveChanges();
            return order;
        }
    }
   
}
