using GoodHamburguer.Business.Models;
using GoodHamburguer.Business.Services.Interfaces;
using GoodHamburguer.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburguer.Data.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly AppDbContext _context;
        public MenuRepository(AppDbContext context)
        {
            _context = context;
        }
        public List<MenuItem> GetAllMenuItems()
        {
            return _context.MenuItems.ToList();
        }
        public List<MenuItem> GetSandwiches()
        {
            return _context.MenuItems.Where(m => m.Type == ItemType.Sandwich).ToList();
        }
        public List<MenuItem> GetExtras()
        {
            return _context.MenuItems.Where(m => m.Type == ItemType.Extra).ToList();
        }
    }
  
}
