using Microsoft.EntityFrameworkCore;
using rappi.Domain.Models;
using rappi.Infrastructure.Data;

namespace rappi.Application.Services;

public class OrderService
{
    private readonly AppDbContext _db;
    public OrderService(AppDbContext db) => _db = db;

    public Task<List<Order>> GetAllAsync() => _db.Orders.Include(o => o.Details).ToListAsync();
    public Task<Order?> GetByIdAsync(int id) => _db.Orders.Include(o => o.Details).FirstOrDefaultAsync(o => o.Id == id);
    public async Task<Order> AddAsync(Order o) { await _db.Orders.AddAsync(o); await _db.SaveChangesAsync(); return o; }
    public async Task<bool> UpdateStatusAsync(int id, string status) { var ex = await _db.Orders.FindAsync(id); if (ex==null) return false; ex.Status = status; await _db.SaveChangesAsync(); return true; }
    public async Task<bool> DeleteAsync(int id) { var e = await _db.Orders.FindAsync(id); if (e==null) return false; _db.Orders.Remove(e); await _db.SaveChangesAsync(); return true; }
}