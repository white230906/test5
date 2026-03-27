using Microsoft.EntityFrameworkCore;
using TetPee.Repository.Entity;

namespace TetPee.Repository;

public class AppDbContext : DbContext
{
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Seller> Sellers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(builder =>
        {
            var newUser = new List<User>()
            {
                new User()
                {
                    Id = Guid.NewGuid(),
                    Email = "admin@gmail.com",
                    Password = "PiedTeam",
                    Role = "Admin"
                }
            };
            builder.HasData(newUser);
        });
    }
}