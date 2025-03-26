namespace EventManager.Api.Models;

public record BookingDto
{
    public required string UserId { get; init; }
    public required string EventId { get; init; }
    public string EventName { get; init; }
    public DateTime EventDate { get; init; }
    public int TicketCount { get; init; }
    public decimal TotalPrice { get; init; }
    public DateTime? CreatedAt { get; init; }
};

public record CreateBookingDto
{
    public required string EventId { get; init; }
    public required int TicketCount { get; init; }
};

public record UpdateBookingDto : BookingDto { };

public record PatchBookingDto
{
    public required string Id { get; init; }
    public string? UserId { get; init; }
    public string? EventId { get; init; }
    public int? TicketCount { get; init; }
    public DateTime? CreatedAt { get; init; }
}

public record BookingResponseDto : BookingDto { }
