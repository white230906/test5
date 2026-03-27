namespace TetPee.Repository.Abstraction;

public class BaseEntity<Tkey>
{
    public Tkey Id { get; set; }
    public bool IsDeleted { get; set; }
}