using Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Presentation.Dto.Rating;

namespace Presentation.Controllers;

[ApiController]
[Route("api")]
public class RatingController : ControllerBase
{
    private readonly IRatingService _service;

    public RatingController(IRatingService service)
    {
        _service = service;
    }

    [HttpPost("ratings")]
    public async Task<ActionResult> RegisterPassengerAsync(PostRatingRequest request, CancellationToken token)
    {
        await _service.PostRatingAsync(request.SubjectType, request.SubjectId, request.RaterId, request.Stars, request.Comment, token);
        return Ok();
    }
}