using Application.Models;
using Application.PasswordHashers;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Security.PasswordHashers;

public class UserPasswordHasher : IUserPasswordHasher
{
    private readonly PasswordHasher<User> _passwordHasher;

    public UserPasswordHasher()
    {
        _passwordHasher = new PasswordHasher<User>();
    }

    public string HashUserPassword(User user, string password)
    {
        return _passwordHasher.HashPassword(user, password);
    }

    public PasswordVerificationResult VerifyHashedUserPassword(
        User user,
        string hashedPassword,
        string providedPassword)
    {
        return _passwordHasher.VerifyHashedPassword(user, user.Password, providedPassword);
    }
}