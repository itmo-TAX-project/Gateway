using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace Presentation.Extensions;

public static class MvcControllerExtensions
{
    public static IServiceCollection AddMvcPresentation(this IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        return services;
    }
}