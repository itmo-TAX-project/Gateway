using Application.Contracts;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddScoped<IUserService, UserService>()
            .AddScoped<IRatingService, RatingService>();
    }
}