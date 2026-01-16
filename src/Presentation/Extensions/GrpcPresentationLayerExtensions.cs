using AccountMaster.Grpc;
using AdminMaster.Grpc;
using Microsoft.Extensions.DependencyInjection;
using PassengerMaster.Grpc;
using Presentation.Grpc.Interceptors;
using Rides.RideService.Contracts;
using TaxiMaster.Grpc;

namespace Presentation.Extensions;

public static class GrpcPresentationLayerExtensions
{
    public static IServiceCollection AddGrpcPresentation(this IServiceCollection services)
    {
        services.AddSingleton<ErrorHandlerInterceptor>();
        services.AddGrpc(options => options.Interceptors.Add<ErrorHandlerInterceptor>());

        services.AddGrpcClient<AccountService.AccountServiceClient>();
        services.AddGrpcClient<PassengerService.PassengerServiceClient>();
        services.AddGrpcClient<TaxiService.TaxiServiceClient>();
        services.AddGrpcClient<AdminService.AdminServiceClient>();
        services.AddGrpcClient<RatingService.Api.Grpc.RatingService.RatingServiceClient>();
        services.AddGrpcClient<RideService.RideServiceClient>();
        services.AddGrpcClient<RideService.RideServiceClient>();

        return services;
    }
}