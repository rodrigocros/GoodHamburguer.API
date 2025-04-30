using GoodHamburgerAPI.Models;
using GoodHamburguer.API.Models;

namespace GoodHamburguer.API.Services.Interfaces
{
    public interface ISvcOrder
    {
        Order? CreateOrder(Request request, out string? error);
        Order? UpdateOrder(int id, Order updatedOrder, out string? error);
        List<Order> GetOrders();
    }
}