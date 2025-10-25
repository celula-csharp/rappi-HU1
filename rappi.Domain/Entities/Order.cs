using System.ComponentModel.DataAnnotations.Schema;

namespace rappi.Domain.Entities;

public class Order
{
    public int Id { get; set; }

    // Clave foránea a Customer
    public int CustomerId { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public Customer Customer { get; set; } = null!;

    public DateTime OrderDate { get; set; }

    // Clave foránea al estado del pedido
    public int StatusId { get; set; }

    [ForeignKey(nameof(StatusId))]
    public OrderStatus Status { get; set; } = null!;

    // Relación 1 a muchos con OrderDetail
    public ICollection<OrderDetail> Details { get; set; } = new List<OrderDetail>();
}