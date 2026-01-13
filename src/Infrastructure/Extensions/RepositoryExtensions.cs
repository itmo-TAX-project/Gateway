using Application.Repositories;
using Infrastructure.Database.Resositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services.AddSingleton<IUserRepository, UserRepository>();
    }
}