using EventManager.Core.Data;
using EventManager.Core.Models;

namespace EventManager.Core.Services;

public interface IBookingService
{
    Task<Booking> CreateBookingAsync(Booking newBooking);
    Task<IEnumerable<Booking>> GetAllUserBookingsAsync(string userId);
    Task<Booking> CancelBookingAsync(string bookingId);
}

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IEventService _eventService;

    public BookingService(IBookingRepository bookingRepository, IEventService eventService)
    {
        _bookingRepository = bookingRepository;
        _eventService = eventService;
    }

    public async Task<Booking> CreateBookingAsync(Booking newBooking)
    {
        var evt = await _eventService.GetEventByIdAsync(newBooking.EventId);
        if (evt is null)
        {
            throw new KeyNotFoundException("Event not found.");
        }
        if (evt.AvailableTickets < newBooking.TicketCount)
        {
            throw new KeyNotFoundException("Not enough tickets available.");
        }

        newBooking.TotalPrice = evt.Price * newBooking.TicketCount;
        newBooking.EventName = evt.Name;
        newBooking.EventDate = evt.StartTime;

        evt.AvailableTickets -= newBooking.TicketCount;
        await _eventService.UpdateEventPartialAsync(evt.Id, evt);

        await _bookingRepository.CreateAsync(newBooking);
        return newBooking;
    }

    // Gets all bookings for a specific user
    public async Task<IEnumerable<Booking>> GetAllUserBookingsAsync(string userId)
    {
        var bookings = await _bookingRepository.GetAllAsync();
        return bookings.Where(b => b.UserId == userId);
    }

    // Cancels a booking
    public async Task<Booking> CancelBookingAsync(string bookingId)
    {
        var booking = await _bookingRepository.GetByIdAsync(bookingId);
        if (booking is null)
        {
            throw new KeyNotFoundException("Booking not found");
        }

        var evt = await _eventService.GetEventByIdAsync(booking.EventId);
        if (evt is null)
        {
            throw new KeyNotFoundException("Event not found.");
        }

        evt.AvailableTickets += booking.TicketCount;
        await _eventService.UpdateEventAsync(evt.Id, evt);

        await _bookingRepository.DeleteAsync(booking.Id);
        return booking;
    }
}
