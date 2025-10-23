using rappi.Domain.Entities;
using rappi.Infrastructure.Repositories;

namespace rappi.Application.Services;

public class CustomerService
{
    private readonly CustomerRepository _repo;
    public CustomerService(CustomerRepository repo) => _repo = repo;
    
    
    public Task<List<Customer>> GetAllAsync(int id) => _repo.GetAllAsync();
    public Task<Customer?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
    public Task<Customer> CreateAsync(Customer c) => _repo.AddAsync(c);
    public Task<bool> UpdateAsync(Customer c) => _repo.UpdateAsync(c);
    public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
}