using Infrastructure.Models;

namespace Application.Kafka.Messages.AccountCreated;

public record AccountCreatedMessageValue(string Name, string Phone, UserRole Role, string? LicenseNumber = null);