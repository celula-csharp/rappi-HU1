namespace rappi.Application.DTOs;

public class OrderCreateDto
{
    public int CustomerId { get; set; }
    public int StatusId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
}