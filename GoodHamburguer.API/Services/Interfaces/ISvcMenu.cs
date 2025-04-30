using GoodHamburgerAPI.Models;

namespace GoodHamburguer.API.Services.Interfaces
{
    public interface ISvcMenu
    {
        Task<List<MenuItem>> GetAllMenuItems();
        Task<List<MenuItem>> GetSandwiches();
        Task<List<MenuItem>> GetExtras();
    }
}