using Application.Models;

namespace Application.Contracts;

public interface IRatingClient
{
    Task<RatingAggregate> GetRatingAsync(long subjectId, CancellationToken cancellationToken);
}