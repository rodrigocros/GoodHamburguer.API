using GoodHamburguer.Business.Services.Rules;
using GoodHamburguer.Business.Models;
using GoodHamburguer.Business.Services.Interfaces;
using System.Runtime;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace GoodHamburguer.Business.Services;

public class SvcOrder : ISvcOrder
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMenuRepository _menuRepository;

    public SvcOrder(IOrderRepository orderRepository, IMenuRepository menuRepository)
    {
        _orderRepository = orderRepository;
        _menuRepository = menuRepository;
    }

    public Order? CreateOrder(Request request, out string? error)
    {
        error = null;
        var   sandwich= _menuRepository.GetSandwiches().FirstOrDefault(i => i.Id == request.SandwichId && i.Type == ItemType.Sandwich);

        if (sandwich == null)
        {
            error = "Invalid sandwich";
            return null;
        }
        decimal total = sandwich.Price;

        if (!DuplicatedItems(request, out var duplicatedError))
        {
            error = duplicatedError;
            return null;
        }

        if (request.FriesId != null)
        {
            var fries = _menuRepository.GetExtras().FirstOrDefault(i => i.Id == request.FriesId && i.Name.Contains("Fries"));
            if (!FriesIsValid(fries, out var friesError))
            {
                error = friesError;
                return null;
            }
            total += fries.Price;
        }

        if (request.SoftDrinkId != null)
        {
            var drink = _menuRepository.GetExtras().FirstOrDefault(i => i.Id == request.SoftDrinkId && i.Name.Contains("Soft Drink"));
            if (!SoftDrinkIsValid(drink, out var drinkError))
            {
                error = drinkError;
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
        order.Total = DiscountRules.ApplyDiscount(request, total);

       _orderRepository.CreateOrder(order);

        return order;
    }



    public void DeleteOrder(int id)
    {
        var existing = _orderRepository.GetOrderById(id);
        if (existing == null)
        {
            throw new Exception("Order not found");
        }
        _orderRepository.DeleteOrder(id);
    }

    public List<Order> GetOrders()
    {
        return _orderRepository.GetOrders();
    }

    public Order? UpdateOrder(int id, Request updatedRequest, out string? error)
    {
        error = null;
        var existing = _orderRepository.GetOrderById(id);
        decimal total = 0;
        if (existing == null)
        {
            error = "Order not found";
            return null;
        }

        var sandwich = _menuRepository.GetSandwiches().FirstOrDefault(i => i.Id == updatedRequest.SandwichId && i.Type == ItemType.Sandwich);
        if (sandwich == null)
        {
            error = "Invalid sandwich";
            return null;
        }
        total = sandwich.Price;

        if (!DuplicatedItems(updatedRequest, out var duplicatedError))
        {
            error = duplicatedError;
            return null;
        }

        if (updatedRequest.FriesId != null)
        {
            var fries = _menuRepository.GetExtras().FirstOrDefault(i => i.Id == updatedRequest.FriesId && i.Name.Contains("Fries"));
            if (!FriesIsValid(fries, out var friesError))
            {
                error = friesError;
                return null;
            }
            total += fries.Price;
        }

        if (updatedRequest.SoftDrinkId != null)
        {
            var drink = _menuRepository.GetExtras().FirstOrDefault(i => i.Id == updatedRequest.SoftDrinkId && i.Name.Contains("Soft Drink"));
            if (!SoftDrinkIsValid(drink, out var drinkError))
            {
                error = drinkError;
                return null;
            }
            total += drink.Price;
        }

        existing.SandwichId = updatedRequest.SandwichId;
        existing.FriesId = updatedRequest.FriesId;
        existing.SoftDrinkId = updatedRequest.SoftDrinkId;
        existing.Total = DiscountRules.ApplyDiscount(updatedRequest, total);

        _orderRepository.UpdateOrder(id, existing);

        return existing;
    }
    private bool DuplicatedItems(Request request,  out  string errorVerifyIfDuplicatedItems)
    {
        errorVerifyIfDuplicatedItems = null;
        if ((request.FriesId != null && request.FriesId == request.SandwichId) ||
         (request.SoftDrinkId != null && request.SoftDrinkId == request.SandwichId) ||
         (request.FriesId != null && request.SoftDrinkId != null && request.FriesId == request.SoftDrinkId))
        {
            errorVerifyIfDuplicatedItems = "Duplicated items in order";
            return false;
        }
        return true;
    }
    private bool SoftDrinkIsValid(MenuItem drink, out string errorVerifyIfSoftDrinkIsValid)
    {
        errorVerifyIfSoftDrinkIsValid = null;
        if (drink == null)
        {
            errorVerifyIfSoftDrinkIsValid = "Invalid soft drink";
            return false;
        }
        return true;
    }
    private bool FriesIsValid(MenuItem fries, out string errorVerifyIfFriesIsValid)
    {
        errorVerifyIfFriesIsValid = null;
        if (fries == null)
        {
            errorVerifyIfFriesIsValid = "Invalid fries";
            return false;
        }
        return true;
    }

}
