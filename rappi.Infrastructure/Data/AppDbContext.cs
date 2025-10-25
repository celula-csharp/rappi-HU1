using Microsoft.EntityFrameworkCore;
using rappi.Domain.Entities;

namespace rappi.Infrastructure.Data;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    
    public DbSet<Customer> customers { get; set; }
    public DbSet<Order> order { get; set; }
    public DbSet<OderDetail> orderDetail { get; set; }

}