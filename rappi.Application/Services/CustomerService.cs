using rappi.Application.Interfaces;
using rappi.Domain.Entities;

namespace rappi.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly IRepository<Customer> _repo;

    public CustomerService(IRepository<Customer> repo) => _repo = repo;

    // Obtener todos los clientes
    public Task<List<Customer>> GetAllAsync() => _repo.GetAllAsync();

    // Obtener cliente por ID
    public Task<Customer?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

    // Crear un nuevo cliente
    public async Task<Customer> AddAsync(Customer c)
    {
        if (string.IsNullOrWhiteSpace(c.Name))
            throw new ArgumentException("El nombre del cliente no puede estar vacío.");
        if (string.IsNullOrWhiteSpace(c.Email))
            throw new ArgumentException("El email del cliente no puede estar vacío.");

        return await _repo.AddAsync(c);
    }

    // Actualizar cliente existente
    public async Task<bool> UpdateAsync(int id, Customer updated)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing == null) return false;

        if (string.IsNullOrWhiteSpace(updated.Name))
            throw new ArgumentException("El nombre no puede estar vacío.");
        if (string.IsNullOrWhiteSpace(updated.Email))
            throw new ArgumentException("El email no puede estar vacío.");

        existing.Name = updated.Name;
        existing.Email = updated.Email;

        return await _repo.UpdateAsync(existing);
    }

    // Eliminar cliente solo si no tiene pedidos asociados
    public async Task<bool> DeleteAsync(int id)
    {
        var customer = await _repo.GetByIdAsync(id);
        if (customer == null) return false;

        if (customer.Orders != null && customer.Orders.Any())
            throw new InvalidOperationException($"No se puede eliminar el cliente con ID {id} porque tiene pedidos asociados.");

        return await _repo.DeleteAsync(id);
    }
}