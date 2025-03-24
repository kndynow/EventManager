using System;
using System.ComponentModel.DataAnnotations;

namespace EventManager.Client.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string UserId { get; set; }
        
        [Range(1, 10, ErrorMessage = "票数必须在1-10之间")]
        public int Quantity { get; set; }
        
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public BookingStatus Status { get; set; }
    }

    public enum BookingStatus
    {
        Pending,
        Confirmed,
        Cancelled
    }
} 