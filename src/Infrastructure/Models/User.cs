namespace Infrastructure.Models;

public class User
{
    public User(string name, string password, UserRole role)
    {
        Name = name;
        Role = role;
        Password = password;
    }
    
    public User(string name, UserRole role)
    {
        Name = name;
        Role = role;
        Password = string.Empty;
    }
    
    public string Name { get; private set; }
    
    public string Password { get; set; }
    
    public UserRole Role { get; private set; }
}