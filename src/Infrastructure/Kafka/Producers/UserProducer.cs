using Application.Models;
using Application.Producers;
using Infrastructure.Kafka.Messages.AccountCreated;
using Itmo.Dev.Platform.Kafka.Extensions;
using Itmo.Dev.Platform.Kafka.Producer;

namespace Infrastructure.Kafka.Producers;

public class UserProducer : IUserProducer
{
    private readonly IKafkaMessageProducer<AccountCreatedMessageKey, AccountCreatedMessageValue> _producer;

    public UserProducer(IKafkaMessageProducer<AccountCreatedMessageKey, AccountCreatedMessageValue> producer)
    {
        _producer = producer;
    }

    public async Task ProduceUserAsync(User user, CancellationToken cancellationToken)
    {
        var messageKey = new AccountCreatedMessageKey();
        var messageValue = new AccountCreatedMessageValue(user.Name, user.PhoneNumber, user.Role, user.LicenseNumber);

        var kafkaMessage = new KafkaProducerMessage<AccountCreatedMessageKey, AccountCreatedMessageValue>(
            messageKey,
            messageValue);

        await _producer.ProduceAsync(kafkaMessage, cancellationToken);
    }
}