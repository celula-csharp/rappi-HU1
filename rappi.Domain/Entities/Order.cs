namespace rappi.Domain.Models;

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    public DateTime OrderDate { get; set; }
    
    // relacion del estado
    public int StatusId { get; set; }
    public OrderStatus Status { get; set; } = null!;
    public ICollection<OrderDetail>? Details { get; set; }
    
}