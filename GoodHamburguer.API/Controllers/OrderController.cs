using Microsoft.AspNetCore.Mvc;
using GoodHamburguer.Business.Models;
using GoodHamburguer.Business.Services.Interfaces;


namespace GoodHamburgerAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{

    private readonly ISvcOrder _orderService;

    public OrderController(ISvcOrder orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public IActionResult CreateOrder([FromBody] Request request)
    {
        if (request == null) return BadRequest("Order cannot be null");
        string? error;
        var createdOrder = _orderService.CreateOrder(request, out error);
        if (createdOrder == null)
        {
            return BadRequest(error);
        }
        return CreatedAtAction(nameof(GetOrders), new { id = createdOrder.Id }, createdOrder);

    }

    [HttpGet]
    public IActionResult GetOrders() => Ok(_orderService.GetOrders());

    [HttpPut("{id}")]
    public IActionResult UpdateOrder(int id, [FromBody] Request updatedRequest)
    {
        if (updatedRequest == null) return BadRequest("Order cannot be null");
        string? error;
        var orderToUpdate= _orderService.GetOrders().FirstOrDefault(i => i.Id == id);
        if (orderToUpdate == null)
        {
            return NotFound("Order not found");
        }

        var orderUpdated = _orderService.UpdateOrder(id, updatedRequest,  out error);

        if (orderUpdated == null)
        {
            return BadRequest(error);
        }

        return Ok(orderUpdated);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteOrder(int id)
    {
        var orderToDelete = _orderService.GetOrders().FirstOrDefault(i => i.Id == id);
        if (orderToDelete == null)
        {
            return NotFound("Order not found");
        }
        _orderService.DeleteOrder(id);
        return NoContent();

    }
}
