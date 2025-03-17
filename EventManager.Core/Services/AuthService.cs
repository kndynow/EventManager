//using EventManager.Core.Data;
//using EventManager.Core.Models;

//namespace EventManager.Core.Services;

//public interface IAuthService
//{
//    User? Login(string username, string password);
//    User? Register(string username, string password);
//}

//public class AuthService : IAuthService
//{
//    private readonly IAuthRepository _authRepository;

//    public AuthService(IAuthRepository authRepository)
//    {
//        _authRepository = authRepository;
//    }

//    public User? Login(string username, string password)
//    {
//        var user = _database.Users.FirstOrDefault(u => u.Username == username);
//        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
//        {
//            return null;
//        }

//        return new User(user.Username, user.Role);
//    }

//    //TODO: Refractor for better readability
//    // Maybe create a create a IValidator / Validator?
//    public User? Register(string username, string password)
//    {
//        if (
//            _database.Users.Any(u => u.Username == username || string.IsNullOrWhiteSpace(username))
//            || string.IsNullOrWhiteSpace(password)
//        )
//        {
//            return null;
//        }

//        var user = new User(username, BCrypt.Net.BCrypt.HashPassword(password));

//        _database.Users.Add(user);
//        return user;
//    }
//}
