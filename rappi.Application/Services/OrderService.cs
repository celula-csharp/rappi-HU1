using rappi.Application.Interfaces;
using rappi.Domain.Entities;

namespace rappi.Application.Services;

public class OrderService : IOrderService
{
    private readonly IRepository<Order> _orders;
    private readonly IRepository<Customer> _customers;
    private readonly IRepository<OrderStatus> _statuses;

    public OrderService(IRepository<Order> orders, IRepository<Customer> customers, IRepository<OrderStatus> statuses)
    {
        _orders = orders;
        _customers = customers;
        _statuses = statuses;
    }

    // Obtener todas las órdenes
    public Task<List<Order>> GetAllAsync() =>
        _orders.GetAllIncludingAsync(o => o.Details, o => o.Status, o => o.Customer);

    // Obtener orden por ID
    public Task<Order?> GetByIdAsync(int id) =>
        _orders.GetByIdIncludingAsync(id, o => o.Details, o => o.Status, o => o.Customer);

    // Crear una nueva orden con validaciones
    public async Task<Order> AddAsync(Order o)
    {
        var customer = await _customers.GetByIdAsync(o.CustomerId);
        if (customer == null)
            throw new ArgumentException($"El cliente con ID {o.CustomerId} no existe.");

        var status = await _statuses.GetByIdAsync(o.StatusId);
        if (status == null)
            throw new ArgumentException($"El estado con ID {o.StatusId} no es válido.");

        if (o.OrderDate == default)
            throw new ArgumentException("La fecha de la orden no es válida.");

        return await _orders.AddAsync(o);
    }

    // Actualizar el estado de una orden
    public async Task<bool> UpdateStatusAsync(int id, int statusId)
    {
        var order = await _orders.GetByIdAsync(id);
        if (order == null) return false;

        var status = await _statuses.GetByIdAsync(statusId);
        if (status == null)
            throw new ArgumentException($"El estado con ID {statusId} no existe.");

        order.StatusId = statusId;
        return await _orders.UpdateAsync(order);
    }

    // Eliminar orden existente
    public Task<bool> DeleteAsync(int id) => _orders.DeleteAsync(id);
    
}