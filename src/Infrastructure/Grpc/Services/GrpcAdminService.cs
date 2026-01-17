using AdminMaster.Grpc;
using Application.Contracts;

namespace Infrastructure.Grpc.Services;

public class GrpcAdminService : IAdminService
{
    private readonly AdminService.AdminServiceClient _client;

    public GrpcAdminService(AdminService.AdminServiceClient client)
    {
        _client = client;
    }

    public async Task BanAccountAsync(long userId, long adminId, CancellationToken token)
    {
        var req = new BanAccountRequest
        {
            SubjectAccountId = userId,
            CreatedByAdminId = adminId,
        };

        await _client.BanAccountAsync(req);
    }

    public async Task UnbanAccountAsync(long userId, long adminId, CancellationToken token)
    {
        var req = new UnbanAccountRequest
        {
            SubjectAccountId = userId,
            CreatedByAdminId = adminId,
        };

        await _client.UnbanAccountAsync(req);
    }

    public async Task MakeRefundAsync(long rideId, long adminId, CancellationToken token)
    {
        var req = new MakeRefundRequest
        {
            RideId = rideId,
            CreatedByAdminId = adminId,
        };

        await _client.MakeRefundAsync(req);
    }
}