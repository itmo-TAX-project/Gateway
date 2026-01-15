using Application.Generators;
using Application.PasswordHashers;
using Infrastructure.Security.JwtGenerators;
using Infrastructure.Security.JwtGenerators.Options;
using Infrastructure.Security.PasswordHashers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
        JwtGeneratorOptions options = services.BuildServiceProvider()
            .GetRequiredService<IOptions<JwtGeneratorOptions>>().Value;

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.JwtKey)),
                };
            });

        services.AddSingleton<IUserPasswordHasher, UserPasswordHasher>();
        services.AddSingleton<IJwtGenerator, JwtGenerator>();

        return services;
    }
}