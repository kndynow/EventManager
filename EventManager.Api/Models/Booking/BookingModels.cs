namespace EventManager.Api.Models;

public record BookingDto
{
    public required string UserId { get; init; }
    public required string EventId { get; init; }
    public string EventName { get; init; }
    public DateTime EventDate { get; init; }
    public int BookedAmount { get; init; }
    public bool IsActive { get; init; }
    public DateTime? CreatedAt { get; init; }
};

public record CreateBookingDto : BookingDto { };

public record UpdateBookingDto : CreateBookingDto { };

public record PatchBookingDto
{
    public required string Id { get; init; }
    public string? UserId { get; init; }
    public string? EventId { get; init; }
    public int? BookedAmount { get; init; }
    public bool? IsActive { get; init; }
    public DateTime? CreatedAt { get; init; }
}

public record BookingResponseDto : BookingDto { }
