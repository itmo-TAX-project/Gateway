using Application.Models;

namespace Application.Generators;

public interface IJwtGenerator
{
    string GenerateJwtToken(User user);
}