namespace EventManager.Api.Models;

public record EventDto
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required EventType Type { get; init; }
    public required DateTime StartTime { get; set; }
    public required DateTime EndTime { get; set; }
    public required int MaxAttendees { get; set; }
};

public record CreateEventDto : EventDto { };

public record UpdateEventDto : CreateEventDto
{
    public required string Id { get; init; }
};

public record PatchEventDto
{
    public required string Id { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public EventType? Type { get; set; }
    public DateTime? StartTime { get; init; }
    public DateTime? EndTime { get; set; }
    public int? MaxAttendees { get; set; }
}

public record EventResponseDto : EventDto
{
    public override string ToString()
    {
        return $"Event: {Name} ({Type}), "
            + $"Time: {StartTime:yyyy-MM-dd HH:mm} to {EndTime:yyyy-MM-dd HH:mm}, "
            + $"Max Attendees: {MaxAttendees}, "
            + $"Description: {Description}";
    }
}
