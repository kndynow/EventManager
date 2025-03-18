using System.Threading.Tasks;
using EventManager.Core.Data;
using EventManager.Core.Models;
using EventManager.Core.Validator;

namespace EventManager.Core.Services;

public interface IUserService
{
    //User login and registration, applies to both admin and user
    Task<User?> Register(string username, string password);
    Task<User?> Login(string username, string password);
}

public class UserService : IUserService
{
    private readonly IUserValidator _userValidator;
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository authRepository, IUserValidator userValidator)
    {
        _userRepository = authRepository;
        _userValidator = userValidator;
    }

    public async Task<User?> Login(string username, string password)
    {
        var user = await _userRepository.GetByUsernameAsync(username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            return null; //Invalid username or password
        }

        return new User(user.Username, user.Role);
    }

    public async Task<User?> Register(string username, string password)
    {
        if (!_userValidator.IsValidUsername(username))
        {
            return null;
        }

        if (!_userValidator.IsValidPassword(password))
        {
            return null;
        }

        //Returns null if username already exists in database
        var existingUser = await _userRepository.GetByUsernameAsync(username);

        if (existingUser != null)
        {
            return null;
        }

        var newUser = new User(username, BCrypt.Net.BCrypt.HashPassword(password));

        await _userRepository.CreateAsync(newUser);
        return newUser;
    }
}
