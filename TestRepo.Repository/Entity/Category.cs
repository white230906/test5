using TetPee.Repository.Abstraction;

namespace TetPee.Repository.Entity;

public class Category: BaseEntity<Guid>, IAuditableEntity
{
    public string Name { get; set; }
    public Guid? ParentId { get; set; }
    
    public Category Parent { get; set; }
    public ICollection<Category> Children { get; set; } = new List<Category>();
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}