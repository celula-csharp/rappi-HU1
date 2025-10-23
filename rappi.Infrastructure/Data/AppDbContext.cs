using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using rappi.Domain.Entities;

namespace rappi.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions option) : base(option){}
    
    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Customer>(eb =>
        {
            eb.HasKey(c => c.Id);
            eb.HasKey(Property(c => c.Name).IsRequired().HasMaxLength(200);
            eb.HasKey(Property(c => c.Email).IsRequired().HasMaxLength(200);
        });
    }
}
