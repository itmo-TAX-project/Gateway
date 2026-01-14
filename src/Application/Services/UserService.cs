using Application.Contracts;
using Application.Kafka.Messages.AccountCreated;
using Confluent.Kafka;
using Infrastructure.Db.Persistence;
using Infrastructure.Models;
using Infrastructure.Security;
using Itmo.Dev.Platform.Kafka.Producer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class UserService : IUserService
{
    private JwtGenerator _jwtGenerator;
    
    private readonly IUserRepository _userRepository;

    private readonly IKafkaMessageProducer<AccountCreatedMessageKey, AccountCreatedMessageValue> _producer;

    public UserService(IUserRepository userRepository, JwtGenerator jwtGenerator, IKafkaMessageProducer<AccountCreatedMessageKey, AccountCreatedMessageValue> producer)
    {
        _userRepository = userRepository;
        _jwtGenerator = jwtGenerator;
        _producer = producer;
    }
    
    public async Task<bool> RegisterPassengerAsync(string name, string phone, string password, CancellationToken cancellationToken)
    {
        var hasher = new PasswordHasher<User>();
        var user = new User(name, UserRole.Passenger);
        user.Password = hasher.HashPassword(user, password);
        
        var result = await _userRepository.AddUserAsync(user, cancellationToken);
        if (!result) return false;
        var messageKey = new AccountCreatedMessageKey();
        var messageValue = new AccountCreatedMessageValue(name, phone, UserRole.Passenger);

        var message = new KafkaProducerMessage<AccountCreatedMessageKey, AccountCreatedMessageValue>(messageKey, messageValue);
        var messageListAsync = AsyncEnumerableEx.Return(message);
            
        await _producer.ProduceAsync(messageListAsync, cancellationToken);
        return true;

    }

    public async Task<bool> RegisterDriverAsync(string name, string phone, string password, string licenseNumber, CancellationToken cancellationToken)
    {
        var hasher = new PasswordHasher<User>();
        var user = new User(name, UserRole.Driver);
        user.Password = hasher.HashPassword(user, password);
        
        var result = await _userRepository.AddUserAsync(user, cancellationToken);
        if (!result) return false;
        var messageKey = new AccountCreatedMessageKey();
        var messageValue = new AccountCreatedMessageValue(name, phone, UserRole.Driver, licenseNumber);

        var message = new KafkaProducerMessage<AccountCreatedMessageKey, AccountCreatedMessageValue>(messageKey, messageValue);
        var messageListAsync = AsyncEnumerableEx.Return(message);
            
        await _producer.ProduceAsync(messageListAsync, cancellationToken);
        return true;

    }

    public async Task<string> LoginAsync(string name, string password, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserAsync(name, cancellationToken);
        if (user == null)
        {
            return string.Empty;
        }
        var hasher = new PasswordHasher<User>();

        var result = hasher.VerifyHashedPassword(user, user.Password, password);
        return result == PasswordVerificationResult.Success ? _jwtGenerator.GenerateJwtToken(user) 
            : throw new AuthenticationFailureException("Login failed.");
    }
}