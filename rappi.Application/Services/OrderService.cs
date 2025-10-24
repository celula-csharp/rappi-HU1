using Microsoft.EntityFrameworkCore;
using rappi.Application.Interfaces;
using rappi.Domain.Models;
using rappi.Infrastructure.Data;

namespace rappi.Application.Services;

public class OrderService : IOrderService
{
    private readonly AppDbContext _db;
    public OrderService(AppDbContext db) => _db = db;

    public Task<List<Order>> GetAllAsync() =>
        _db.Orders
            .Include(o => o.Details)
            .Include(o => o.Status)
            .Include(o => o.Customer)
            .ToListAsync();

    public Task<Order?> GetByIdAsync(int id) =>
        _db.Orders
            .Include(o => o.Details)
            .Include(o => o.Status)
            .Include(o => o.Customer)
            .FirstOrDefaultAsync(o => o.Id == id);

    public async Task<Order> AddAsync(Order o)
    {
        // Validación: Cliente debe existir
        var customerExists = await _db.Customers.AnyAsync(c => c.Id == o.CustomerId);
        if (!customerExists)
            throw new ArgumentException($"El cliente con ID {o.CustomerId} no existe.");

        // Validación: Estado debe existir
        var statusExists = await _db.OrderStatus.AnyAsync(s => s.Id == o.StatusId);
        if (!statusExists)
            throw new ArgumentException($"El estado con ID {o.StatusId} no es válido.");

        // Validación: fecha
        if (o.OrderDate == default)
            throw new ArgumentException("La fecha de la orden no es válida.");

        await _db.Orders.AddAsync(o);
        await _db.SaveChangesAsync();
        return o;
    }

    public async Task<bool> UpdateStatusAsync(int id, int statusId)
    {
        var order = await _db.Orders.FindAsync(id);
        if (order == null) return false;

        var statusExists = await _db.OrderStatus.AnyAsync(s => s.Id == statusId);
        if (!statusExists)
            throw new ArgumentException($"El estado con ID {statusId} no existe.");

        order.StatusId = statusId;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var order = await _db.Orders
            .Include(o => o.Details)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null) return false;

        _db.Orders.Remove(order);
        await _db.SaveChangesAsync();
        return true;
    }
}
