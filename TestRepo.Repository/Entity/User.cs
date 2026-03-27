using TetPee.Repository.Abstraction;

namespace TetPee.Repository.Entity;

public class User: BaseEntity<Guid>, IAuditableEntity
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } = "User";
    
    public Seller? Seller { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}