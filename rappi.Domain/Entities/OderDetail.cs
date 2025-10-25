using System.ComponentModel.DataAnnotations.Schema;

namespace rappi.Domain.Entities;

public class OderDetail
{
    public int id { get; set; }
    
    //Configuracion de la Fk 
    [ForeignKey("Order")]
    public int OrderId { get; set; } 
    public virtual Order order { get; set; }
    
    
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public double UnitPrice { get; set; }


}