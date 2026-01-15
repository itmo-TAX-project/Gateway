namespace Application.Contracts;

public interface IRatingService
{
    Task PostRatingAsync(string? subjectType, long subjectId, long raterId, int stars, string? comment, CancellationToken token);
}