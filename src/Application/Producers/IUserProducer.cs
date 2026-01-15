using Application.Models;

namespace Application.Producers;

public interface IUserProducer
{
    Task ProduceUserAsync(User user, CancellationToken cancellationToken);
}