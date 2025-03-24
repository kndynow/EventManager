using System;

namespace EventManager.Client.Models;

public class Event
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public string ShortDescription { get; set; }
    public required DateTime Start { get; set; }
    public required DateTime End { get; set; }
    public required string Location { get; set; }
    public required decimal Price { get; set; }
    public required int AvailableTickets { get; set; }
    public required int MaxAttendees { get; set; }
    public string? ImageUrl { get; set; }
    public string? Category { get; set; }
    public required bool IsActive { get; set; }
    public required DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
