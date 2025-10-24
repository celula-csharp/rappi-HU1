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
        if (string.IsNullOrWhiteSpace(s.Name))
            throw new ArgumentException("El nombre del estado no puede estar vacío.");

        // Validar duplicado
        var exists = await _db.OrderStatus.AnyAsync(x => x.Name.ToLower() == s.Name.ToLower());
        if (exists)
            throw new InvalidOperationException($"El estado '{s.Name}' ya existe.");

        await _db.OrderStatus.AddAsync(s);
        await _db.SaveChangesAsync();
        return s;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var status = await _db.OrderStatus
            .Include(s => s.Orders)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (status == null) return false;
        if (status.Orders != null && status.Orders.Any())
            throw new InvalidOperationException($"No se puede eliminar el estado '{status.Name}' porque está asociado a órdenes existentes.");

        _db.OrderStatus.Remove(status);
        await _db.SaveChangesAsync();
        return true;
    }
}