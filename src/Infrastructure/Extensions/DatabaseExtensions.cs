using Infrastructure.Database.Migrations;
using Infrastructure.Database.Options;
using Infrastructure.Extensions.Plugins;
using Itmo.Dev.Platform.Persistence.Abstractions.Extensions;
using Itmo.Dev.Platform.Persistence.Postgres.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.Extensions;

public static class DatabaseExtensions
{
    public static IServiceCollection AddPostgresPersistence(this IServiceCollection services)
    {
        services.AddPlatformPersistence(
            persistence => persistence.UsePostgres(
                postgres => postgres.WithConnectionOptions(
                        builder =>
                        {
                            DatabaseConfigOptions options = services.BuildServiceProvider()
                                .GetRequiredService<IOptions<DatabaseConfigOptions>>().Value;

                            builder.Configure(connectionOptions =>
                            {
                                connectionOptions.Host = options.Host;
                                connectionOptions.Port = options.Port;
                                connectionOptions.Database = options.Database;
                                connectionOptions.Username = options.Username;
                                connectionOptions.Password = options.Password;
                                connectionOptions.SslMode = options.SslMode;
                            });
                        })
                    .WithMigrationsFrom(typeof(Initial).Assembly)
                    .WithDataSourcePlugin<UserRoleMappingPlugin>()
                    .WithDataSourcePlugin<LegacyTimestampBehaviorPlugin>()));

        return services;
    }
}