using Infrastructure.Models;

namespace Application.Contracts;

public interface IUserService
{
    Task<bool> RegisterPassengerAsync(string name, string phone, string password, CancellationToken cancellationToken);
    
    Task<bool> RegisterDriverAsync(string name, string phone, string password, CancellationToken cancellationToken);
    
    Task<string> LoginAsync(string name, string password, CancellationToken cancellationToken);
}