using Application.Models.Enums;

namespace Application.Models;

public class User
{
    public string Name { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public UserRole Role { get; set; }

    public string? LicenseNumber { get; set; } = null;
}