using Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Dto.Admin;
using System.Net;

namespace Presentation.Controllers;

[ApiController]
[Route("api")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _service;

    public AdminController(IAdminService service)
    {
        _service = service;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("admin/ban")]
    public async Task<ActionResult> BanAccountAsync(BanAccountRequest request, CancellationToken token)
    {
        await _service.BanAccountAsync(request.UserId, request.AdminId, token);
        return Ok(HttpStatusCode.OK);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("admin/unban")]
    public async Task<ActionResult> UnbanAccountAsync(UnbanAccountRequest request, CancellationToken token)
    {
        await _service.UnbanAccountAsync(request.UserId, request.AdminId, token);
        return Ok(HttpStatusCode.OK);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("admin/refund")]
    public async Task<ActionResult> MakeRefundAsync(MakeRefundRequest request, CancellationToken token)
    {
        await _service.MakeRefundAsync(request.RideId, request.AdminId, token);
        return Ok(HttpStatusCode.OK);
    }
}