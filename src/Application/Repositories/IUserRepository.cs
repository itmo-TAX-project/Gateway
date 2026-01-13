using Application.Models;

namespace Application.Repositories;

public interface IUserRepository
{
    Task<bool> AddUserAsync(User user, CancellationToken cancellationToken);

    Task UpdateUserAsync(User user, CancellationToken cancellationToken);

    Task<User?> GetUserAsync(string name, CancellationToken cancellationToken);
}