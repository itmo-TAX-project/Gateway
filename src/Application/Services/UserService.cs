using Application.Contracts;
using Application.Generators;
using Application.Models;
using Application.Models.Enums;
using Application.PasswordHashers;
using Application.Producers;
using Application.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IJwtGenerator _jwtGenerator;

    private readonly IUserRepository _userRepository;

    private readonly IUserProducer _userProducer;

    private readonly IUserPasswordHasher _userPasswordHasher;

    public UserService(
        IUserRepository userRepository,
        IJwtGenerator jwtGenerator,
        IUserProducer userProducer,
        IUserPasswordHasher userPasswordHasher)
    {
        _userRepository = userRepository;
        _jwtGenerator = jwtGenerator;
        _userProducer = userProducer;
        _userPasswordHasher = userPasswordHasher;
    }

    public async Task<bool> RegisterPassengerAsync(
        string name,
        string phone,
        string password,
        CancellationToken cancellationToken)
    {
        var user = new User
        {
            Name = name,
            PhoneNumber = phone,
            Role = UserRole.Passenger,
            Password = password,
        };
        user.Password = _userPasswordHasher.HashUserPassword(user, password);

        bool result = await _userRepository.AddUserAsync(user, cancellationToken);
        if (!result) return false;

        await _userProducer.ProduceUserAsync(user, cancellationToken);

        return true;
    }

    public async Task<bool> RegisterAdminAsync(string name, string phone, string password, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Name = name,
            PhoneNumber = phone,
            Role = UserRole.Admin,
            Password = password,
        };
        user.Password = _userPasswordHasher.HashUserPassword(user, password);

        bool result = await _userRepository.AddUserAsync(user, cancellationToken);
        if (!result) return false;

        await _userProducer.ProduceUserAsync(user, cancellationToken);

        return true;
    }

    public async Task<bool> RegisterDriverAsync(
        string name,
        string phone,
        string password,
        string licenseNumber,
        CancellationToken cancellationToken)
    {
        var user = new User
        {
            Name = name,
            PhoneNumber = phone,
            Role = UserRole.Driver,
            Password = password,
            LicenseNumber = licenseNumber,
        };
        user.Password = _userPasswordHasher.HashUserPassword(user, password);

        bool result = await _userRepository.AddUserAsync(user, cancellationToken);
        if (!result) return false;

        await _userProducer.ProduceUserAsync(user, cancellationToken);

        return true;
    }

    public async Task<string> LoginAsync(
        string name,
        string password,
        CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetUserAsync(name, cancellationToken);
        if (user == null)
        {
            throw new NullReferenceException("User not found");
        }

        PasswordVerificationResult result = _userPasswordHasher.VerifyHashedUserPassword(user, user.Password, password);
        return result == PasswordVerificationResult.Success ?
            _jwtGenerator.GenerateJwtToken(user)
            : throw new AuthenticationFailureException("Login failed.");
    }
}