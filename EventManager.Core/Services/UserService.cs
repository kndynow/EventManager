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

    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(string id);
    Task<User> UpdateUserPartialAsync(string id, User updatedUser);
    Task<User> DeleteUserAsync(string id);
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

    // Handles the user login
    public async Task<User?> Login(string username, string password)
    {
        var user = await _userRepository.GetByUsernameAsync(username);

        if (user is null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            return null; //Invalid username or password
        }

        return user;
    }

    // Handles the user registration
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

        if (existingUser is not null)
        {
            return null;
        }

        var newUser = new User(username, BCrypt.Net.BCrypt.HashPassword(password));

        await _userRepository.CreateAsync(newUser);
        return newUser;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<User> GetUserByIdAsync(string id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        //If event is not found, throw an exception
        if (user is null)
        {
            throw new KeyNotFoundException("User not found.");
        }

        return user;
    }

    public async Task<User> UpdateUserPartialAsync(string id, User updatedUser)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user is null)
        {
            throw new KeyNotFoundException("User not found.");
        }

        await _userRepository.UpdatePartialAsync(updatedUser);
        return updatedUser;
    }

    public async Task<User> DeleteUserAsync(string id)
    {
        var user = await GetUserByIdAsync(id);
        if (user is null)
        {
            throw new KeyNotFoundException("User not found.");
        }

        await _userRepository.DeleteAsync(id);
        return user;
    }
}
