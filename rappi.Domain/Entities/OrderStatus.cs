namespace rappi.Domain.Models;

public class OrderStatus
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    // relacion inversa
    public ICollection<Order>? Orders { get; set; }
}