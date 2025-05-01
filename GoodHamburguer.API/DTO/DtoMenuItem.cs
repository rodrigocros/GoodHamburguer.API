using GoodHamburguer.Business.Models;

namespace GoodHamburguer.API.DTO
{
    public class DtoMenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
