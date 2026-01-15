using Application.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.PasswordHashers;

public interface IUserPasswordHasher
{
    string HashUserPassword(User user, string password);

    PasswordVerificationResult VerifyHashedUserPassword(User user, string hashedPassword, string providedPassword);
}