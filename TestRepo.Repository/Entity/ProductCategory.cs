using TetPee.Repository.Abstraction;

namespace TetPee.Repository.Entity;

public class ProductCategory: BaseEntity<Guid>, IAuditableEntity
{
    
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public Product Product { get; set; }
    public Guid ProductId { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}