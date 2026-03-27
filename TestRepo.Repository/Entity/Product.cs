using TetPee.Repository.Abstraction;

namespace TetPee.Repository.Entity;

public class Product: BaseEntity<Guid>, IAuditableEntity
{
    public string Name { get; set; } 
    public decimal Price { get; set; }
    
    public Seller Seller { get; set; }
    public Guid SellerId { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}