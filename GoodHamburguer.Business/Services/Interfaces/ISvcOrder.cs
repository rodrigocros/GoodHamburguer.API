

using GoodHamburguer.Business.Models;

namespace GoodHamburguer.Business.Services.Interfaces
{
    public interface ISvcOrder
    {
        Order? CreateOrder(Request request, out string? error);
        Order? UpdateOrder(int id, Request updateRequest, out string? error);
        List<Order> GetOrders();
        void DeleteOrder(int id);
    }
}