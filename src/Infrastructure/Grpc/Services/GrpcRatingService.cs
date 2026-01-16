using Application.Contracts;
using RatingService.Api.Grpc;
using RatingAggregate = Application.Models.RatingAggregate;

namespace Infrastructure.Grpc.Services;

public class GrpcRatingService : IRatingClient
{
    private readonly RatingService.Api.Grpc.RatingService.RatingServiceClient _client;

    public GrpcRatingService(RatingService.Api.Grpc.RatingService.RatingServiceClient client)
    {
        _client = client;
    }

    public async Task<RatingAggregate> GetRatingAsync(long subjectId, CancellationToken cancellationToken)
    {
        var grpcRequest = new GetRatingRequest
        {
            SubjectId = subjectId,
        };
        GetRatingResponse resp = await _client.GetRatingAsync(grpcRequest);

        return new RatingAggregate(resp.Aggregate.SubjectId, resp.Aggregate.Avg, resp.Aggregate.Count);
    }
}