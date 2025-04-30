using Microsoft.AspNetCore.Mvc;
using GoodHamburgerAPI.Data;
using GoodHamburgerAPI.Models;
using GoodHamburgerAPI.Services;
using GoodHamburguer.API.Services.Interfaces;

namespace GoodHamburgerAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ISvcOrder _orderService;

    public OrderController(AppDbContext context, ISvcOrder orderService)
    {
        _context = context;
        _orderService = orderService;
    }

    [HttpPost]
    public IActionResult CreateOrder([FromBody] Order order)
    {
        if (order == null) return BadRequest("Order cannot be null");
        string? error;
        var createdOrder = _orderService.CreateOrder(order, out error);
        if (createdOrder == null)
        {
            return BadRequest(error);
        }
        return CreatedAtAction(nameof(GetOrders), new { id = createdOrder.Id }, createdOrder);

    }

    [HttpGet]
    public IActionResult GetOrders() => Ok(_orderService.GetOrders());

    [HttpPut("{id}")]
    public IActionResult UpdateOrder(int id, [FromBody] Order updatedOrder)
    {
        if (updatedOrder == null) return BadRequest("Order cannot be null");
        string? error;
        var order = _orderService.UpdateOrder(id, updatedOrder, out error);
        if (order == null)
        {
            return NotFound(error);
        }
        return Ok(order);

    }

    [HttpDelete("{id}")]
    public IActionResult DeleteOrder(int id)
    {
        var order = _context.Orders.Find(id);
        if (order == null)
        {
            return NotFound("Order not found");
        }
        _context.Orders.Remove(order);
        _context.SaveChanges();
        return NoContent();

    }
}
