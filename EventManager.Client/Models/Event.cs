using System.Text.Json.Serialization;

namespace EventManager.Client.Models;

public class Event
{
    public string Id { get; set; } = string.Empty;
    public required string Name { get; set; } = string.Empty;
    public required string Description { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public EventType? Type { get; set; }
    public required DateTime StartTime { get; set; }
    public required DateTime EndTime { get; set; }
    public required string Location { get; set; }
    public required decimal Price { get; set; }
    public required int AvailableTickets { get; set; }
    public required int MaxAttendees { get; set; }
    public string? ImageUrl { get; set; }
    public required bool IsActive { get; set; }
    public required DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EventType
{
    Concert = 0,
    Festival,
    Theatre,
    Sport,
    Other,
}
