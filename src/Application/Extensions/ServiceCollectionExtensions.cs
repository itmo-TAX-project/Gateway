using Application.Kafka.Messages.AccountCreated;
using Itmo.Dev.Platform.Kafka.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddOutboxProduce<TMessageKey, TMessageValue>(this IServiceCollection collection, IConfiguration configuration)
    {
        return collection.AddPlatformKafka(builder => builder
            .ConfigureOptions(configuration.GetSection("Kafka"))
            .AddProducer(b => b
                .WithKey<TMessageKey>()
                .WithValue<TMessageValue>()
                .WithConfiguration(configuration.GetSection("Kafka:Producers:Message"))
                .SerializeKeyWithNewtonsoft()
                .SerializeValueWithNewtonsoft()
                .WithOutbox()));
    }

    public static IServiceCollection AddKafka(this IServiceCollection collection, IConfiguration configuration)
    {
        collection.AddOutboxProduce<AccountCreatedMessageKey, AccountCreatedMessageValue>(configuration);

        return collection;
    }
    
}