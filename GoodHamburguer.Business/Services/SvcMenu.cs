
using GoodHamburguer.Business.Models;
using GoodHamburguer.Business.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburguer.Business.Services
{
    public class SvcMenu : ISvcMenu
    {
        private readonly IMenuRepository _menuRepository;
        public SvcMenu(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }       

        public List<MenuItem> GetAllMenuItems()
        {
            return _menuRepository.GetAllMenuItems();
        }

        public List<MenuItem> GetExtras()
        {
            return _menuRepository.GetExtras();   
        }

        public List<MenuItem> GetSandwiches()
        {
            return _menuRepository.GetSandwiches();
        }
    }
}
