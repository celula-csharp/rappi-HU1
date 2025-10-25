using rappi.Application.Interfaces;
using rappi.Domain.Models;

namespace rappi.Application.Services;

public class OrderStatusService
{
    private readonly IRepository<OrderStatus> _repo;

    public OrderStatusService(IRepository<OrderStatus> repo) => _repo = repo;

    // Obtener todos los estados
    public Task<List<OrderStatus>> GetAllAsync() => _repo.GetAllAsync();

    // Obtener estado por ID
    public Task<OrderStatus?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

    // Crear un nuevo estado
    public async Task<OrderStatus> AddAsync(OrderStatus s)
    {
        if (string.IsNullOrWhiteSpace(s.Name))
            throw new ArgumentException("El nombre del estado no puede estar vacío.");

        var all = await _repo.GetAllAsync();
        if (all.Any(x => x.Name.Equals(s.Name, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException($"El estado '{s.Name}' ya existe.");

        return await _repo.AddAsync(s);
    }
    
    public async Task<OrderStatus> UpdateAsync(int id, string name)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing == null) throw new ArgumentException($"Estado con ID {id} no encontrado.");

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("El nombre del estado no puede estar vacío.");

        // Validar duplicado (excepto el propio)
        var all = await _repo.GetAllAsync();
        if (all.Any(x => x.Id != id && x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException($"El estado '{name}' ya existe.");

        existing.Name = name;
        await _repo.UpdateAsync(existing);
        return existing;
    }

    // Eliminar estado si no está asociado a órdenes
    public async Task<bool> DeleteAsync(int id)
    {
        var status = await _repo.GetByIdAsync(id);
        if (status == null) return false;

        if (status.Orders != null && status.Orders.Any())
            throw new InvalidOperationException($"No se puede eliminar el estado '{status.Name}' porque está asociado a órdenes.");

        return await _repo.DeleteAsync(id);
    }
}