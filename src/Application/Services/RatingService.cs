using Application.Contracts;
using Application.Models;
using Application.Producers;

namespace Application.Services;

public class RatingService : IRatingService
{
    private readonly IRatingProducer _producer;

    public RatingService(IRatingProducer producer)
    {
        _producer = producer;
    }

    public async Task PostRatingAsync(
        string? subjectType,
        long subjectId,
        long raterId,
        int stars,
        string? comment,
        CancellationToken token)
    {
        var rating = new Rating
        {
            SubjectType = subjectType ?? string.Empty,
            SubjectId = subjectId,
            RaterId = raterId,
            Stars = stars,
            Comment = comment ?? string.Empty,
        };

        await _producer.ProduceRatingAsync(rating, token);
    }
}