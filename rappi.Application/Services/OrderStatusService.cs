using Microsoft.EntityFrameworkCore;
using rappi.Domain.Models;
using rappi.Infrastructure.Data;

namespace rappi.Application.Services;

public class OrderStatusService
{
    private readonly AppDbContext _db;
    public OrderStatusService(AppDbContext db) => _db = db;

    public Task<List<OrderStatus>> GetAllAsync() => _db.OrderStatus.ToListAsync();

    public Task<OrderStatus?> GetByIdAsync(int id) =>
        _db.OrderStatus.FirstOrDefaultAsync(s => s.Id == id);

    public async Task<OrderStatus> AddAsync(OrderStatus s)
    {
        await _db.OrderStatus.AddAsync(s);
        await _db.SaveChangesAsync();
        return s;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var e = await _db.OrderStatus.FindAsync(id);
        if (e == null) return false;

        _db.OrderStatus.Remove(e);
        await _db.SaveChangesAsync();
        return true;
    }
}