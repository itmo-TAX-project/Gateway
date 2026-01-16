using Application.Contracts;
using Application.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.Dto.Rating;
using System.Net;

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
    public async Task<ActionResult> PostRatingAsync(PostRatingRequest request, CancellationToken token)
    {
        await _service.PostRatingAsync(request.SubjectType, request.SubjectId, request.RaterId, request.Stars, request.Comment, token);
        return Ok(HttpStatusCode.OK);
    }

    [HttpGet("ratings/{subjectId}")]
    public async Task<ActionResult> GetRatingAsync(long subjectId, CancellationToken token)
    {
        RatingAggregate agg = await _service.GetRatingAsync(subjectId, token);
        return Ok(agg);
    }
}