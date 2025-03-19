namespace EventManager.Client.Models;

public class Event
{
    public string? Id { get; set; }
    public required string Name { get; set; } = string.Empty;
    public required string Description { get; set; } = string.Empty;
    public EventType Type { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int MaxAttendees { get; set; }
}

public enum EventType
{
    Concert = 0,
    Festival,
    Theatre,
    Other,
}
