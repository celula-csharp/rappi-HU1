namespace rappi.Domain.Models;

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; } = "Pendiente";

    public ICollection<OrderDetail>? Details { get; set; }
}