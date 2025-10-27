using HelloWorld.Data.Entities;
using HelloWorld.Entities;
using Microsoft.EntityFrameworkCore;

namespace HelloWorld.Data;

public class OrdersDbContext : DbContext
{
    
    public DbSet<CityEntity> Cities { get; set; } = null!;
    public DbSet<OrderEntity> Orders { get; set; } = null!;
    
    public OrdersDbContext(DbContextOptions<OrdersDbContext> options)
        : base(options)
    {
    }
}