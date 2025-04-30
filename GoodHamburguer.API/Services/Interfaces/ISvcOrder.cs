using GoodHamburgerAPI.Models;

namespace GoodHamburguer.API.Services.Interfaces
{
    public interface ISvcOrder
    {
        Order? CreateOrder(Order order, out string? error);
        Order? UpdateOrder(int id, Order updatedOrder, out string? error);
        List<Order> GetOrders();
    }
}