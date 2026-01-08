using Infrastructure.Models;

namespace Infrastructure.Db.Persistence;

public interface IUserRepository
{
    Task<bool> AddUserAsync(User account, CancellationToken cancellationToken);
    
    Task UpdateUserAsync(User user, CancellationToken cancellationToken);
    
    Task<User?> GetUserAsync(string name, CancellationToken cancellationToken);
}