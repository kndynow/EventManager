using EventManager.Core.Data;
using EventManager.Core.Models;

namespace EventManager.Core.Services;

public interface IAuthService
{
    User? Login(string username, string password);
    User? Register(string username, string password);
}

public class AuthService : IAuthService
{
    private readonly IDatabase _database;

    public AuthService(IDatabase database)
    {
        _database = database;
    }

    public User? Login(string username, string password)
    {
        var user = _database.Users.FirstOrDefault(u => u.Username == username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            return null;
        }

        return new User(user.Username, user.Role);
    }

    public User? Register(string username, string password)
    {
        if (
            _database.Users.Any(u => u.Username == username || string.IsNullOrWhiteSpace(username))
            || string.IsNullOrWhiteSpace(password)
        )
        {
            return null;
        }

        var user = new User(username, BCrypt.Net.BCrypt.HashPassword(password));

        _database.Users.Add(user);
        return user;
    }
}
