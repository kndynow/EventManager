using System;
using System.IO.Pipelines;
using EventManager.Core.Data;
using EventManager.Core.Models;

namespace EventManager.Test.Core.Models;

public class UserModelTest
{
    private readonly IAuthRepository _database;

    public UserModelTest()
    {
        _database = new AuthRepository();
    }

    //Test if GUID generation for User ID do not generate identical GUID for multiple users
    [Fact]
    public void GenerateValid_GUID_ShouldReturnFalse()
    {
        //Arrange
        var user1 = new User("testUser1", "validPassword123");
        var user2 = new User("testUser2", "validPassword123");
        var user3 = new User("testUser3", "validPassword123");
        //Act
        _database.CreateAsync(user1);
        var userIds = _database.GetByUsernameAsync(user1.Username).Result.Id;
        //Assert
        Assert.All(userIds, id => Assert.NotEqual(Guid.Empty, id));
    }
}
