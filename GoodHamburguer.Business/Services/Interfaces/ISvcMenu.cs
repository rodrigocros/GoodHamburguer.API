using GoodHamburguer.Business.Models;

namespace GoodHamburguer.Business.Services.Interfaces
{
    public interface ISvcMenu
    {
        List<MenuItem> GetAllMenuItems();
        List<MenuItem> GetSandwiches();
        List<MenuItem> GetExtras();
    }
}