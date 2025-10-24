using Microsoft.EntityFrameworkCore;
using rappi.Application.Interfaces;
using rappi.Domain.Models;
using rappi.Infrastructure.Data;

namespace rappi.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly AppDbContext _db;
    public CustomerService(AppDbContext db) => _db = db;

    public Task<List<Customer>> GetAllAsync() =>
        _db.Customers.Include(c => c.Orders).ToListAsync();

    public Task<Customer?> GetByIdAsync(int id) =>
        _db.Customers.Include(c => c.Orders)
            .FirstOrDefaultAsync(c => c.Id == id);

    public async Task<Customer> AddAsync(Customer c)
    {
        // Validación básica
        if (string.IsNullOrWhiteSpace(c.Name))
            throw new ArgumentException("El nombre del cliente no puede estar vacío.");

        if (string.IsNullOrWhiteSpace(c.Email))
            throw new ArgumentException("El email del cliente no puede estar vacío.");

        await _db.Customers.AddAsync(c);
        await _db.SaveChangesAsync();
        return c;
    }

    public async Task<bool> UpdateAsync(int id, Customer updated)
    {
        var existing = await _db.Customers.FindAsync(id);
        if (existing == null) return false;

        if (string.IsNullOrWhiteSpace(updated.Name))
            throw new ArgumentException("El nombre no puede estar vacío.");

        if (string.IsNullOrWhiteSpace(updated.Email))
            throw new ArgumentException("El email no puede estar vacío.");

        existing.Name = updated.Name;
        existing.Email = updated.Email;

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var customer = await _db.Customers
            .Include(c => c.Orders)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (customer == null) return false;
        if (customer.Orders != null && customer.Orders.Any())
            throw new InvalidOperationException($"No se puede eliminar el cliente con ID {id} porque tiene pedidos asociados.");

        _db.Customers.Remove(customer);
        await _db.SaveChangesAsync();
        return true;
    }
}
