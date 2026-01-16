using Application.Contracts;
using Application.Models;
using Application.Producers;

namespace Application.Services;

public class RatingService : IRatingService
{
    private readonly IRatingProducer _producer;
    private readonly IRatingClient _client;

    public RatingService(IRatingProducer producer, IRatingClient client)
    {
        _producer = producer;
        _client = client;
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

    public async Task<RatingAggregate> GetRatingAsync(
        long subjectId,
        CancellationToken token)
    {
        RatingAggregate agg = await _client.GetRatingAsync(subjectId, token);
        return agg;
    }
}