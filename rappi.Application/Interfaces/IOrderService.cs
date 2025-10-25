using rappi.Domain.Models;

namespace rappi.Application.Interfaces;

public interface IOrderService
{
    Task<List<Order>> GetAllAsync();
    Task<Order?> GetByIdAsync(int id);
    Task<Order> AddAsync(Order o);
    Task<bool> UpdateStatusAsync(int id, int statusId);
    Task<bool> DeleteAsync(int id);
}