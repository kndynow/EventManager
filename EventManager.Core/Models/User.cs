using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EventManager.Core.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; } = UserRoles.User; // Default role
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User(string name, string pwd)
    {
        Username = name;
        PasswordHash = pwd;
    }
}

// Static class to define roles
public static class UserRoles
{
    public const string Admin = "Admin";
    public const string User = "User";
}
