using Application.Models;
using Application.Producers;
using Infrastructure.Kafka.Messages.RatingPosted;
using Itmo.Dev.Platform.Kafka.Extensions;
using Itmo.Dev.Platform.Kafka.Producer;

namespace Infrastructure.Kafka.Producers;

public class RatingProducer : IRatingProducer
{
    private readonly IKafkaMessageProducer<RatingPostedMessageKey, RatingPostedMessageValue> _producer;

    public RatingProducer(IKafkaMessageProducer<RatingPostedMessageKey, RatingPostedMessageValue> producer)
    {
        _producer = producer;
    }

    public async Task ProduceRatingAsync(Rating rating, CancellationToken token)
    {
        var key = new RatingPostedMessageKey();

        var value = new RatingPostedMessageValue
        {
            SubjectType = rating.SubjectType,
            SubjectId = rating.SubjectId,
            RaterId = rating.RaterId,
            Stars = rating.Stars,
            Comment = rating.Comment,
        };

        var message = new KafkaProducerMessage<RatingPostedMessageKey, RatingPostedMessageValue>(key, value);

        await _producer.ProduceAsync(message, token);
    }
}