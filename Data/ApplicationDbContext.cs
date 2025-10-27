using HelloWorld.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HelloWorld.Data;

public sealed class ApplicationDbContext : DbContext
{ 
    public DbSet<StudentEntity> Students { get; set; } = null!;
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}