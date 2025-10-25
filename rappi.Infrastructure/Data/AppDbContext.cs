using Microsoft.EntityFrameworkCore;
using rappi.Domain.Entities;

namespace rappi.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<OrderStatus> OrderStatus { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Customer → Orders
        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Orders)
            .WithOne(o => o.Customer)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        // Order → OrderStatus
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Status)
            .WithMany(s => s.Orders)
            .HasForeignKey(o => o.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        // Propiedades específicas
        modelBuilder.Entity<OrderDetail>().Property(d => d.ProductName).HasMaxLength(200);

        // Datos iniciales para OrderStatus
        modelBuilder.Entity<OrderStatus>().HasData(
            new OrderStatus { Id = 1, Name = "Pendiente" },
            new OrderStatus { Id = 2, Name = "En preparación" },
            new OrderStatus { Id = 3, Name = "Entregado" },
            new OrderStatus { Id = 4, Name = "Cancelado" }
        );
    }
}