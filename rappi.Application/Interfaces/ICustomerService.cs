using rappi.Domain.Models;

namespace rappi.Application.Interfaces;

public interface ICustomerService
{
    Task<List<Customer>> GetAllAsync();
    Task<Customer?> GetByIdAsync(int id);
    Task<Customer> AddAsync(Customer c);
    Task<bool> UpdateAsync(int id, Customer updated);
    Task<bool> DeleteAsync(int id);
}