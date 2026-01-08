using Infrastructure.Models;

namespace Application.Kafka.Messages.AccountCreated;

public record AccountCreatedMessageValue(string Name, String Phone, UserRole Role);