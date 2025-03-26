using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EventManager.Core.Models;

public class Booking
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string UserId { get; set; }
    public string EventId { get; set; }
    public string EventName { get; set; }
    public DateTime EventDate { get; set; }
    public int BookedAmount { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
