namespace Application.Contracts;

public interface IAdminService
{
    Task BanAccountAsync(long userId, long adminId, CancellationToken token);

    Task UnbanAccountAsync(long userId, long adminId, CancellationToken token);

    Task MakeRefundAsync(long rideId, long adminId, CancellationToken token);
}