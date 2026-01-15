using Infrastructure.Kafka.Messages.AccountCreated;
using Itmo.Dev.Platform.Common.Extensions;
using Itmo.Dev.Platform.Kafka.Extensions;
using Itmo.Dev.Platform.MessagePersistence;
using Itmo.Dev.Platform.MessagePersistence.Postgres.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class KafkaExtensions
{
    public static IServiceCollection AddKafka(this IServiceCollection collection, IConfiguration configuration)
    {
        collection.AddOutboxProduce<AccountCreatedMessageKey, AccountCreatedMessageValue>(configuration);

        return collection;
    }

    public static IServiceCollection AddMessagePersistence(this IServiceCollection services)
    {
        services.AddUtcDateTimeProvider();
        services.AddSingleton(new Newtonsoft.Json.JsonSerializerSettings());

        services.AddPlatformMessagePersistence(builder => builder
            .WithDefaultPublisherOptions("MessagePersistence:Publisher:Default")
            .UsePostgresPersistence(
                configurator => configurator.ConfigureOptions("MessagePersistence")));

        return services;
    }

    private static IServiceCollection AddOutboxProduce<TMessageKey, TMessageValue>(this IServiceCollection collection, IConfiguration configuration)
    {
        return collection.AddPlatformKafka(builder => builder
            .ConfigureOptions(configuration.GetSection("Kafka"))
            .AddProducer(b => b
                .WithKey<TMessageKey>()
                .WithValue<TMessageValue>()
                .WithConfiguration(configuration.GetSection("Kafka:Producers:AccountCreatedMessage"))
                .SerializeKeyWithNewtonsoft()
                .SerializeValueWithNewtonsoft()
                .WithOutbox()));
    }
}