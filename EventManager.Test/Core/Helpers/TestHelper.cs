using BCrypt.Net;
using EventManager.Core.Data;
using EventManager.Core.Models;
using EventManager.Core.Services;

namespace EventManager.Test.Core.Helpers;

/// <summary>
/// Provides helper methods for creating and managing test data and test environments.
/// </summary>
/// <remarks>
/// This class contains static methods to simplify the creation of test data
/// and configure test environments in a consistent manner.
/// </remarks>
public class TestHelper
{
    /// <summary>
    /// Creates a new database instance pre-filled with standard test data.
    /// </summary>
    /// <returns>An initialized <see cref="IDatabase"/> with test data.</returns>
    /// <example>
    /// <code>
    /// IDatabase db = TestHelper.CreateTestDatabase();
    /// var users = db.Users.ToList(); // Contains predefined test users
    /// </code>
    /// </example>
    public static IDatabase CreateTestDatabase()
    {
        var database = new Database();
        GenerateTestData(database);
        return database;
    }

    /// <summary>
    /// Creates a new test user with customizable values.
    /// </summary>
    /// <param name="username">Username for the test account. Default is "testUser".</param>
    /// <param name="password">Password for the test account. Default is "TestPassword123!".</param>
    /// <param name="role">User role. Default is <see cref="UserRoles.User"/>.</param>
    /// <returns>A new <see cref="User"/> instance with hashed password.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="username"/> is null.</exception>
    /// <example>
    /// <code>
    /// // Create standard test user
    /// var user1 = TestHelper.CreateTestUser();
    ///
    /// // Create custom test user
    /// var admin = TestHelper.CreateTestUser("admin", "SecurePass123!", UserRoles.Admin);
    /// </code>
    /// </example>
    public static User CreateTestUser(
        string username = "testUser",
        string password = "TestPassword123!",
        string role = UserRoles.User
    )
    {
        var user = new User(username, BCrypt.Net.BCrypt.HashPassword(password)) { Role = role };
        return user;
    }

    /// <summary>
    /// Creates a test event with customizable values.
    /// </summary>
    /// <param name="name">Name of the event. Default is "Test Event".</param>
    /// <param name="type">Type of event. Default is <see cref="EventType.Other"/>.</param>
    /// <param name="maxAttendees">Maximum number of attendees. Default is 100.</param>
    /// <returns>A new <see cref="Event"/> instance.</returns>
    /// <remarks>
    /// Start and end time are automatically set to tomorrow,
    /// where the event lasts for 2 hours by default.
    /// </remarks>
    /// <example>
    /// <code>
    /// // Create a standard test event
    /// var event1 = TestHelper.CreateTestEvent();
    ///
    /// // Create a concert for 200 people
    /// var concert = TestHelper.CreateTestEvent("Summer Concert", EventType.Concert, 200);
    /// </code>
    /// </example>
    public static Event CreateTestEvent(
        string name = "Test Event",
        EventType type = EventType.Other,
        int maxAttendees = 100
    )
    {
        return new Event
        {
            Name = name,
            Type = type,
            MaxAttendees = maxAttendees,
            StartTime = DateTime.UtcNow.AddDays(1),
            EndTime = DateTime.UtcNow.AddDays(1).AddHours(2),
        };
    }

    /// <summary>
    /// Creates a complete test enviroment with initialised database and related services.
    /// </summary>
    /// <returns>
    /// A tuple containing:
    /// <list type="bullet">
    ///   <item><description>An instance of <see cref="IDatabase"/> with test</description></item>
    ///   <item><description>An instance of <see cref="IAuthService"/> connected to the database</description></item>
    /// </list>
    /// </returns>
    /// <remarks>
    /// Use this method when you need a complete test environment with both database and authentication service.
    /// The database will be pre-populated with standard test data.
    /// </remarks>
    /// <example>
    /// <code>
    /// var (database, authService) = TestHelper.CreateTestEnvironment();
    /// // Use this enviroment in your tests.
    /// </code>
    /// </example>
    public static (IDatabase database, IAuthService authService) CreateTestEnvironment()
    {
        var database = CreateTestDatabase();
        var authService = new AuthService(database);

        return (database, authService);
    }

    /// <summary>
    /// Fills the database with standard test data.
    /// </summary>
    /// <param name="database">The database to be filled with test data.</param>
    /// <remarks>
    /// This method adds:
    /// <list type="bullet">
    ///   <item><description>An administrator and two standard users</description></item>
    ///   <item><description>A concert and a festival as test events</description></item>
    /// </list>
    /// </remarks>
    private static void GenerateTestData(IDatabase database)
    {
        database.Users.Add(CreateTestUser("admin", "AdminPass123!", UserRoles.Admin));
        database.Users.Add(CreateTestUser("testUser1", "UserPass123"));
        database.Users.Add(CreateTestUser("testUser2", "UserPass123!"));

        database.Events.Add(CreateTestEvent("Consert", EventType.Concert, 200));
        database.Events.Add(CreateTestEvent("Festival", EventType.Festival, 1000));
    }

    /// <summary>
    /// Validate that user got correct initial values
    /// </summary>
    /// <param name="user"></param>
    public static void ValidateUserBasics(User user)
    {
        Assert.NotNull(user);
        Assert.NotEqual(Guid.Empty, user.Id);
        Assert.NotNull(user.Username);
        Assert.NotNull(user.PasswordHash);
        Assert.NotEqual(DateTime.MinValue, user.CreatedAt);
    }
}
