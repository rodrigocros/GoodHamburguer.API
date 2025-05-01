using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoodHamburguer.Business.Models;

namespace GoodHamburguer.Business.Services.Interfaces
{
    public interface IOrderRepository
    {
        Order CreateOrder(Order order);
        Order UpdateOrder(int id, Order updatedOrder);
        List<Order> GetOrders();
        Order GetOrderById(int id);
        bool DeleteOrder(int id);

    }
}
