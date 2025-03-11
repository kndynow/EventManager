using EventManager.Core.Models;

namespace EventManager.Core.Data;

public interface IDatabase
{
    List<Event> Events { get; set; }
    List<User> Users { get; set; }
}

public class Database : IDatabase
{
    public List<Event> Events { get; set; } = new List<Event>();
    public List<User> Users { get; set; } = new List<User>();
}
