namespace Application.Services.Interfaces;

public interface IUserService
{
    Task<bool> RegisterPassengerAsync(string name, string phone, string password, CancellationToken cancellationToken);

    Task<bool> RegisterDriverAsync(string name, string phone, string password, CancellationToken cancellationToken);
}