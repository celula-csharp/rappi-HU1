using rappi.Application.Interfaces;
using rappi.Domain.Models;
using rappi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace rappi.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly AppDbContext _db;
    public CustomerService(AppDbContext db) => _db = db;

    public Task<List<Customer>> GetAllAsync() => _db.Customers.ToListAsync();

    public Task<Customer?> GetByIdAsync(int id) => _db.Customers.FindAsync(id).AsTask();

    public async Task<Customer> AddAsync(Customer c)
    {
        await _db.Customers.AddAsync(c);
        await _db.SaveChangesAsync();
        return c;
    }

    public async Task<bool> UpdateAsync(int id, Customer updated)
    {
        var existing = await _db.Customers.FindAsync(id);
        if (existing == null) return false;

        existing.Name = updated.Name;
        existing.Email = updated.Email;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var e = await _db.Customers.FindAsync(id);
        if (e == null) return false;

        _db.Customers.Remove(e);
        await _db.SaveChangesAsync();
        return true;
    }
}