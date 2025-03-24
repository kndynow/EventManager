using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Models;

namespace EventManager.Client.Services
{
    public interface IBookingService
    {
        Task<List<Booking>> GetBookingsAsync();
        Task<Booking> GetBookingByIdAsync(int id);
        Task<Booking> CreateBookingAsync(Booking booking);
        Task<Booking> UpdateBookingAsync(Booking booking);
        Task DeleteBookingAsync(int id);
        Task<List<Booking>> GetUserBookingsAsync(string userId);
        Task<bool> CancelBookingAsync(int id);
    }
} 