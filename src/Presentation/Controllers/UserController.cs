using Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Dto.Users;

namespace Presentation.Controllers;

[ApiController]
[Route("api")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register/passenger")]
    public async Task<ActionResult> RegisterPassengerAsync(RegisterPassengerRequest request, CancellationToken token)
    {
        bool response = await _userService.RegisterPassengerAsync(request.Name, request.Phone, request.Password, token);
        return Ok(response);
    }

    [HttpPost("register/driver")]
    public async Task<ActionResult> RegisterDriverAsync(RegisterDriverRequest request, CancellationToken token)
    {
        bool response = await _userService.RegisterDriverAsync(request.Name, request.Phone, request.Password, request.LicenseNumber, token);
        return Ok(response);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("register/admin")]
    public async Task<ActionResult> RegisterAdminAsync(RegisterAdminRequest registerRequest, CancellationToken token)
    {
        bool response = await _userService.RegisterAdminAsync(registerRequest.Name, registerRequest.Phone, registerRequest.Password, token);
        return Ok(response);
    }

    [HttpGet("login")]
    public async Task<ActionResult<string>> Login(AuthRequest request, CancellationToken token)
    {
        string response = await _userService.LoginAsync(request.Name, request.Password, token);
        return response;
    }
}