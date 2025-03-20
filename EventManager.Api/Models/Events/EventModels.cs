namespace EventManager.Api.Models;

public record EventDto
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required EventType Type { get; init; }
    public required DateTime Start { get; init; }
    public required DateTime End { get; init; }
    public required int MaxAttendees { get; init; }
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
    public EventType? Type { get; init; }
    public DateTime? Start { get; init; }
    public DateTime? End { get; init; }
    public int? MaxAttendees { get; init; }
}

public record EventResponseDto : EventDto { }
