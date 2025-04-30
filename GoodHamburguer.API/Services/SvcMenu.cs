using GoodHamburgerAPI.Data;
using GoodHamburgerAPI.Models;
using GoodHamburguer.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburguer.API.Services
{
    public class SvcMenu : ISvcMenu
    {
        public AppDbContext _context;
        public SvcMenu(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<MenuItem>> GetAllMenuItems()
        {
            return  await _context.MenuItems.ToListAsync();
        }

        public async Task<List<MenuItem>> GetExtras()
        {
            return await _context.MenuItems
                .Where(x => x.Type == ItemType.Extra)
                .ToListAsync();
        }

        public Task<List<MenuItem>> GetSandwiches()
        {
            return _context.MenuItems
                .Where(x => x.Type == ItemType.Sandwich)
                .ToListAsync();
        }
    }
}
