using GoodHamburgerAPI.Data;
using GoodHamburgerAPI.Models;
using GoodHamburguer.API.Models;
using GoodHamburguer.API.Services.Interfaces;
using GoodHamburguer.API.Services.Rules;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GoodHamburgerAPI.Services;

public class SvcOrder : ISvcOrder
{
    private readonly AppDbContext _context;

    public SvcOrder(AppDbContext context)
    {
        _context = context;
    }

    public Order? CreateOrder(Request request, out string? error)
    {
        error = null;

        var sandwich = _context.MenuItems.FirstOrDefault(i => i.Id == request.SandwichId && i.Type == ItemType.Sandwich);
        if (sandwich == null)
        {
            error = "Invalid sandwich";
            return null;
        }

        if (!VerifyIfDuplicatedItems(request))
        {
            error = "Duplicated items in order";
            return null;
        }
     
        decimal total = sandwich.Price;

        if (request.FriesId != null)
        {
            var fries = _context.MenuItems.FirstOrDefault(i => i.Id == request.FriesId && i.Name.Contains("Fries"));
            if (fries == null)
            {
                error = "Invalid fries";
                return null;
            }
            total += fries.Price;
        }

        if (request.SoftDrinkId != null)
        {
            var drink = _context.MenuItems.FirstOrDefault(i => i.Id == request.SoftDrinkId && i.Name.Contains("Soft Drink"));
            if (drink == null)
            {
                error = "Invalid soft drink";
                return null;
            }
            total += drink.Price;
        }

        Order order = new Order
        {
            SandwichId = request.SandwichId,
            FriesId = request.FriesId,
            SoftDrinkId = request.SoftDrinkId
        };
        order.Total = DiscountRules.ApplyDiscount(order, total);

        _context.Orders.Add(order);
        _context.SaveChanges();

        return order;
    }

    public List<Order> GetOrders()
    {
        var orders = new List<Order>();
        foreach (var order in _context.Orders)
        {
            var sandwich = _context.MenuItems.FirstOrDefault(i => i.Id == order.SandwichId);
            var fries = order.FriesId != null ? _context.MenuItems.FirstOrDefault(i => i.Id == order.FriesId) : null;
            var drink = order.SoftDrinkId != null ? _context.MenuItems.FirstOrDefault(i => i.Id == order.SoftDrinkId) : null;
            orders.Add(new Order
            {
                Id = order.Id,
                SandwichId = sandwich.Id,
                FriesId = fries?.Id,
                SoftDrinkId = drink?.Id,
                Total = order.Total
            });
        }
        return orders;

    }

    public Order? UpdateOrder(int id, Order updatedOrder, out string? error)
    {
        error = null;
        var existing = _context.Orders.FirstOrDefault(o => o.Id == id);
        if (existing == null)
        {
            error = "Order not found";
            return null;
        }

        var sandwich = _context.MenuItems.FirstOrDefault(i => i.Id == updatedOrder.SandwichId && i.Type == ItemType.Sandwich);
        if (sandwich == null)
        {
            error = "Invalid sandwich";
            return null;
        }

        decimal total = sandwich.Price;

        if (updatedOrder.FriesId != null)
        {
            var fries = _context.MenuItems.FirstOrDefault(i => i.Id == updatedOrder.FriesId && i.Name.Contains("Fries"));
            if (fries == null)
            {
                error = "Invalid fries";
                return null;
            }
            total += fries.Price;
        }

        if (updatedOrder.SoftDrinkId != null)
        {
            var drink = _context.MenuItems.FirstOrDefault(i => i.Id == updatedOrder.SoftDrinkId && i.Name.Contains("Soft Drink"));
            if (drink == null)
            {
                error = "Invalid soft drink";
                return null;
            }
            total += drink.Price;
        }

        existing.SandwichId = updatedOrder.SandwichId;
        existing.FriesId = updatedOrder.FriesId;
        existing.SoftDrinkId = updatedOrder.SoftDrinkId;
        existing.Total = DiscountRules.ApplyDiscount(updatedOrder, total);

        _context.SaveChanges();
        return existing;
    }

    private bool VerifyIfDuplicatedItems(Request request)
    {
        if ((request.FriesId != null && request.FriesId == request.SandwichId) ||
         (request.SoftDrinkId != null && request.SoftDrinkId == request.SandwichId) ||
         (request.FriesId != null && request.SoftDrinkId != null && request.FriesId == request.SoftDrinkId))
        {
            return false;
        }
        return true;
    }

}
