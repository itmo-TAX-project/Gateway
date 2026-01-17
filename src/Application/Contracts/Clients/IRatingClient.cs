using Application.Models;

namespace Application.Contracts.Clients;

public interface IRatingClient
{
    Task<RatingAggregate> GetRatingAsync(long subjectId, CancellationToken cancellationToken);
}