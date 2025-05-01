using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoodHamburguer.Business.Models;

namespace GoodHamburguer.Business.Services.Interfaces
{
    public interface IMenuRepository
    {
        List<MenuItem> GetAllMenuItems();
        List<MenuItem> GetSandwiches();
        List<MenuItem> GetExtras();
    }
}
