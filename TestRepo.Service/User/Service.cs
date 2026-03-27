using Microsoft.EntityFrameworkCore;
using TetPee.Repository;

namespace TetPee.Service.User;

public class Service: IService
{
    private readonly AppDbContext _dbContext;
    public Service(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<string> CreatUser(Request.UserRequest userRequest)
    {
        var queryEmailExist = _dbContext.Users.Where(x => x.Email == userRequest.Email);
        var existEmail = await queryEmailExist.AnyAsync();
        if (existEmail)
        {
            throw new Exception("Email already exists");
        }

        var newUser = new Repository.Entity.User()
        {
            Email = userRequest.Email,
            Password = userRequest.Password,
            Role = "User"
        };
        _dbContext.Add(newUser);
        await _dbContext.SaveChangesAsync();
        return Response.Message.Created;
        
    }
}