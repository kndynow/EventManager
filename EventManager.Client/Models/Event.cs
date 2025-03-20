using System.Text.Json.Serialization;

namespace EventManager.Client.Models;

public class Event
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public EventType Type { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int MaxAttendees { get; set; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EventType
{
    Concert = 0,
    Festival,
    Theatre,
    Other,
}
