using Microsoft.AspNetCore.Mvc;
using GoodHamburguer.Business.Services.Interfaces;
using GoodHamburguer.API.DTO;

namespace GoodHamburguer.Controllers;

[ApiController]
[Route("[controller]")]
public class MenuController : ControllerBase
{
    private readonly ISvcMenu _svcMenu;

    public MenuController(ISvcMenu svcMenu)
    {
        _svcMenu = svcMenu;
    }

    [HttpGet("all")]
    public IActionResult GetMenu()
    {
        var menuItems = _svcMenu.GetAllMenuItems();
        if (menuItems == null)
        {
            return NotFound("No menu items found");
        }
       var dtoMenuItem = menuItems.Select(i => new DtoMenuItem() 
        {
            Id = i.Id,
            Name = i.Name,
            Price = i.Price
        });
        return Ok(dtoMenuItem);
    }

    [HttpGet("sandwiches")]
    public IActionResult GetSandwiches(){
        var sandwiches = _svcMenu.GetSandwiches();
        if (sandwiches == null)
        {
            return NotFound("No sandwiches found");
        }
        var dtoMenuItem = sandwiches.Select(i => new DtoMenuItem()
        {
            Id = i.Id,
            Name = i.Name,
            Price = i.Price
        });
        return Ok(dtoMenuItem);
    }

    [HttpGet("extras")]
    public IActionResult GetExtras()
    {
        var extras = _svcMenu.GetExtras();
        if (extras == null)
        {
            return NotFound("No extras found");
        }
        var dtoMenuItem = extras.Select(i => new DtoMenuItem()
        {
            Id = i.Id,
            Name = i.Name,
            Price = i.Price
        });
        return Ok(dtoMenuItem);
    }
}
