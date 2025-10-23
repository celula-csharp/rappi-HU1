using rappi.Domain.Entities;

namespace rappi.Infrastructure.Repositories;

public class CustomerRepository
{
    private readonly AppContext _db;
    public CustomerRepository(AppContext db) => _db = db;

    public async Task<List<Customer>> GetAllAsync() =>
        await _db.Customers.AsNoTracking().TiListAsync();

    public async Task<Customer?> GetByIdAsync(int id) =>
        await _db.Customers.FindAsync(id);

    public async Task<Customer> AddAsync(Customer customer)
    {
        _db.Customers.Add(customer);
        await _db.SaveChangesAsync();
        return customer;
    }

    public async Task<bool> UpdateAsync(Customer customer)
    {
        var existing = await _db.Customers.FindAsync(customer.Id);
        if (existing == null) return false;

        existing.Name = customer.Name;
        existing.Email = customer.Email;

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _db.Customers.FindAsync(id);
        if (existing == null) return false;

        _db.Customers.Remove(existing);
        await _db.SaveChangesAsync();
        return true;
    }
}