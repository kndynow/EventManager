using System.ComponentModel.DataAnnotations;

namespace EventManager.Client.Models;

public class BookingModel
{
    [Required]
    public string EventId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
    public int Quantity { get; set; }
} 