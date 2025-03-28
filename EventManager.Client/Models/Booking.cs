using System;
using System.ComponentModel.DataAnnotations;

namespace EventManager.Client.Models;

public class Booking
{
    public string Id { get; set; } = string.Empty;
    public string EventId { get; set; } = string.Empty;
    public string EventName { get; set; } = string.Empty;
    public DateTime EventDate { get; set; }
    public int TicketCount { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime? CreatedAt { get; set; }

    [Required]
    [Range(1, 8, ErrorMessage = "Please select between 1 and 8 tickets")]
    public int BookedAmount { get; set; } = 1;
}
