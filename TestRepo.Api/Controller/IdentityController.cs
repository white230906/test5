using Microsoft.AspNetCore.Mvc;
using TetPee.Repository;
using TetPee.Service.Identity;

namespace TestRepo.Api.Controller;

[ApiController]
[Route("[controller]")]
public class IdentityController: ControllerBase
{
    public readonly IService _identityService;

    public IdentityController(IService identityService)
    {
        _identityService = identityService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(Request.UserLoginRequest userLoginRequest)
    {
        var token = await _identityService.Login(userLoginRequest);
        return Ok(token);
    }
    
}