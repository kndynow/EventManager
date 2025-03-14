using EventManager.Core.Data;
using EventManager.Core.Models;
using System.Threading.Tasks;

namespace EventManager.Core.Services;

public interface IAuthService
{
    //User? Login(string username, string password);
    Task<User?> Register(string username, string password);
    Task<User?> Login(string username, string password);
}

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;

    public AuthService(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<User?> Login(string username, string password)
    {
        var user = await _authRepository.GetByUsernameAsync(username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            return null; //Invalid username or password
        }

        return new User(user.Username, user.Role);
    }

    // TODO: Create validator for user registration via Validator/IValidator
    public async Task<User?> Register(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            return null; // Invalid username or password
        }

        //Returns null if username already exists in database
        var existingUser = await _authRepository.GetByUsernameAsync(username);

        if (existingUser != null)
        {
            return null;
        }

        var newUser = new User(username, BCrypt.Net.BCrypt.HashPassword(password));

        await _authRepository.CreateAsync(newUser);
        return newUser;
    }
}
