using GoodHamburguer.Business.Models;
using GoodHamburguer.Business.Services;
using GoodHamburguer.Business.Services.Interfaces;
using GoodHamburguer.Data.Context;
using GoodHamburguer.Data.Repositories;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("GoodHamburgerDB"));
builder.Services.AddScoped<ISvcOrder, SvcOrder>();
builder.Services.AddScoped<ISvcMenu, SvcMenu>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

// Seed data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    db.MenuItems.AddRange(new[]
    {
        new MenuItem { Id = 1, Name = "X Burger", Price = 5.00m, Type = ItemType.Sandwich },
        new MenuItem { Id = 2, Name = "X Egg", Price = 4.50m, Type = ItemType.Sandwich },
        new MenuItem { Id = 3, Name = "X Bacon", Price = 7.00m, Type = ItemType.Sandwich },
        new MenuItem { Id = 4, Name = "Fries", Price = 2.00m, Type = ItemType.Extra },
        new MenuItem { Id = 5, Name = "Soft Drink", Price = 2.50m, Type = ItemType.Extra },
    });

    db.SaveChanges();
}

app.Run();
