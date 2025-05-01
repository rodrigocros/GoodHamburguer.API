using Microsoft.AspNetCore.Mvc;
using GoodHamburguer.Business.Services.Interfaces;

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
    public IActionResult GetMenu() => Ok(_svcMenu.GetAllMenuItems());

    [HttpGet("sandwiches")]
    public IActionResult GetSandwiches() =>
        Ok(_svcMenu.GetSandwiches());

    [HttpGet("extras")]
    public IActionResult GetExtras() =>
        Ok(_svcMenu.GetExtras());
}
