namespace EventManager.Api.Models;

public record EventDto
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public string ShortDescription { get; init; }
    public required EventType Type { get; init; }
    public required DateTime StartTime { get; init; }
    public required DateTime EndTime { get; init; }
    public required string Location { get; init; }
    public required decimal Price { get; init; }
    public required int AvailableTickets { get; init; }
    public required int MaxAttendees { get; init; }
    public string? ImageUrl { get; init; }
    public required bool IsActive { get; init; }
    public required DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
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
    public string? ShortDescription { get; init; }
    public EventType? Type { get; init; }
    public DateTime? StartTime { get; init; }
    public DateTime? EndTime { get; init; }
    public string? Location { get; init; }
    public decimal? Price { get; init; }
    public int? AvailableTickets { get; init; }
    public int? MaxAttendees { get; init; }
    public string? ImageUrl { get; init; }
    public bool? IsActive { get; init; }
    public DateTime? CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public record EventResponseDto : EventDto { }
