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
    public async Task<ActionResult> RegisterPassengerAsync(RegisterPassengerRequest request)
    {
        bool response = await _userService.RegisterPassengerAsync(request.Name, request.Phone, request.Password, default);
        return Ok(response);
    }

    [HttpPost("register/driver")]
    public async Task<ActionResult> RegisterDriverAsync(RegisterPassengerRequest request)
    {
        bool response = await _userService.RegisterDriverAsync(request.Name, request.Phone, request.Password, request.LicenseNumber, default);
        return Ok(response);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("register/admin")]
    public async Task<ActionResult> RegisterAdminAsync(RegisterAdminRequest registerRequest)
    {
        bool response = await _userService.RegisterAdminAsync(registerRequest.Name, registerRequest.Phone, registerRequest.Password, default);
        return Ok(response);
    }

    public async Task<ActionResult<string>> Login(AuthRequest request)
    {
        string response = await _userService.LoginAsync(request.Name, request.Password, default);
        return response;
    }
}