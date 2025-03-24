using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EventManager.Core.Models;

public class Event
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public string ShortDescription { get; set; }
    public required EventType Type { get; set; }
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
