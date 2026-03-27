using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TetPee.Repository;
using TetPee.Service.JwtService;

namespace TetPee.Service.Identity;

public class Service: IService
{
    private readonly AppDbContext _dbContext;
    private readonly JwtOptions _jwtOptions = new();
    private readonly JwtService.IService _tokenService;

    public Service(AppDbContext dbContext, JwtService.IService tokenService, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _tokenService = tokenService;
        configuration.GetSection(nameof(JwtOptions)).Bind(_jwtOptions);
    }
    
    public async Task<Response.UserResponse> Login(Request.UserLoginRequest request)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        if (user.Password != request.Password)
        {
            throw new Exception("Wrong password");
        }

        var claims = new List<Claim>()
        {
            new Claim("UserId", user.Id.ToString()),
            new Claim("Email", user.Email),
            new Claim("Role", user.Role),
            new Claim(ClaimTypes.Role, user.Role),
        };
        var token = _tokenService.GenerateAccessToken(claims);
        return new Response.UserResponse()
        {
            AccessToken = token,
        };
        
    }
}