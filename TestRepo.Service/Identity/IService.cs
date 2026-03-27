namespace TetPee.Service.Identity;

public interface IService
{
    public Task<Response.UserResponse> Login(Request.UserLoginRequest request);
}