namespace GoodHamburgerAPI.Models;

public enum ItemType { Sandwich, Extra }

public class MenuItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public ItemType Type { get; set; }
}
