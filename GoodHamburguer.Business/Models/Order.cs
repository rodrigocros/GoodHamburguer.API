namespace GoodHamburguer.Business.Models;

public class Order
{
    public int Id { get; set; }
    public int SandwichId { get; set; }
    public int? FriesId { get; set; }
    public int? SoftDrinkId { get; set; }
    public decimal Total { get; set; }
}
