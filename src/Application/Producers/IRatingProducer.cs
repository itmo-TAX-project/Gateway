using Application.Models;

namespace Application.Producers;

public interface IRatingProducer
{
    Task ProduceRatingAsync(Rating rating, CancellationToken token);
}