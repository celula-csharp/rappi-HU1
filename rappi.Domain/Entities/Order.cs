using System.ComponentModel.DataAnnotations.Schema;

namespace rappi.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    
    //configuracion de la FK
    [ForeignKey("Customer")]
    public int CustomerId { get; set; } // fk
    public virtual Customer customer { get; set; }  //
    
    
    public DateTime OrderDate { get; set; }
    public string Status { get; set; }
}