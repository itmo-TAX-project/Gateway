using AccountMaster.Grpc;
using AdminMaster.Grpc;
using Application.Contracts;
using Application.Contracts.Clients;
using Infrastructure.Grpc.Interceptors;
using Infrastructure.Grpc.Services;
using Microsoft.Extensions.DependencyInjection;
using PassengerMaster.Grpc;
using Rides.RideService.Contracts;
using TaxiMaster.Grpc;

namespace Infrastructure.Extensions;

public static class GrpcExtensions
{
    public static IServiceCollection AddGrpcPresentation(this IServiceCollection services)
    {
        services.AddSingleton<ErrorHandlerInterceptor>();
        services.AddGrpc(options => options.Interceptors.Add<ErrorHandlerInterceptor>());

        services.AddGrpcClient<AccountService.AccountServiceClient>();
        services.AddGrpcClient<PassengerService.PassengerServiceClient>();
        services.AddGrpcClient<TaxiService.TaxiServiceClient>();
        services.AddGrpcClient<AdminService.AdminServiceClient>(o => o.Address = new Uri("http://localhost:8050"));
        services.AddGrpcClient<RatingService.Api.Grpc.RatingService.RatingServiceClient>(o => o.Address = new Uri("http://localhost:8021"));
        services.AddGrpcClient<RideService.RideServiceClient>();

        services.AddScoped<IRatingClient, GrpcRatingService>();
        services.AddScoped<IAdminService, GrpcAdminService>();

        return services;
    }
}