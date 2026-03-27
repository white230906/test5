namespace TetPee.Service.User;

public interface IService
{
    public Task<string> CreatUser(Request.UserRequest userRequest);
   
}