using Microsoft.EntityFrameworkCore;
using GoodHamburgerAPI.Models;

namespace GoodHamburgerAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<MenuItem> MenuItems => Set<MenuItem>();
    public DbSet<Order> Orders => Set<Order>();
}
