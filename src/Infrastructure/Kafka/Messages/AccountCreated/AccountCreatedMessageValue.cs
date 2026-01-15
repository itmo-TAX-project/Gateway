using Application.Models.Enums;

namespace Infrastructure.Kafka.Messages.AccountCreated;

public record AccountCreatedMessageValue(string Name, string Phone, UserRole Role, string? LicenseNumber);