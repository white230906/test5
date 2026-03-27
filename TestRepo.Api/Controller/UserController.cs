using Microsoft.AspNetCore.Mvc;
using TetPee.Service.User;

namespace TestRepo.Api.Controller;

[ApiController]
[Route("[controller]")]
public class UserController: ControllerBase
{
    private readonly IService _userService;
    public UserController(IService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(Request.UserRequest userRequest)
    {
        var newUser = await _userService.CreatUser(userRequest);
        return Ok(newUser);
    }

}