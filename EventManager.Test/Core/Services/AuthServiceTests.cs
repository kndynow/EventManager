using System;
using EventManager.Core.Data;
using EventManager.Core.Models;
using EventManager.Core.Services;
using EventManager.Test.Core.Helpers;

namespace EventManager.Test.Core.Services;

public class AuthServiceTests
{
    private readonly IDatabase _database;
    private readonly IAuthService _authService;

    public AuthServiceTests()
    {
        var (db, auth) = TestHelper.CreateTestEnvironment();
        _database = db;
        _authService = auth;
    }

    //Valid user registration
    [Fact]
    public void RegisterUser_WithValidData_ShouldCreateNewUser()
    {
        //Arrange
        var testUser = TestHelper.CreateTestUser();

        //Act
        var result = _authService.Register(testUser.Username, "validPassword123");

        //Assert
        Assert.NotNull(result);
        TestHelper.ValidateUserBasics(result);
        Assert.Equal(UserRoles.User, result.Role);
    }

    //Invalid user registration
    [Theory]
    [InlineData("", "password123")]
    [InlineData("user", "")]
    [InlineData(null, "password123")]
    [InlineData("user", null)]
    public void RegisterUser_WithInvalidData_ShouldReturnNull(string username, string password)
    {
        //Act
        var result = _authService.Register(username, password);
        //Assert
        Assert.Null(result);
    }
}
