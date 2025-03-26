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

    // Creates a new booking
    public async Task<Booking> CreateBookingAsync(Booking newBooking)
    {
        // Get specified event
        var evt = await _eventService.GetEventByIdAsync(newBooking.EventId);
        // If event is not found, throw an exception
        if (evt is null)
        {
            throw new KeyNotFoundException("Event not found.");
        }
        // If there are not enough tickets available, throw an exception
        if (evt.AvailableTickets < newBooking.BookedAmount)
        {
            throw new KeyNotFoundException("Not enough tickets available.");
        }
        // Update booking with event name and date
        newBooking.EventName = evt.Name;
        newBooking.EventDate = evt.StartTime;

        // Update event with new available tickets
        evt.AvailableTickets -= newBooking.BookedAmount;
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
        // Get specified booking and check if it exists
        var booking = await _bookingRepository.GetByIdAsync(bookingId);
        if (booking is null || !booking.IsActive)
        {
            throw new KeyNotFoundException("Booking not found or cancelled.");
        }

        // Get event specified in booking and check if it exists
        var evt = await _eventService.GetEventByIdAsync(booking.EventId);
        if (evt is null)
        {
            throw new KeyNotFoundException("Event not found.");
        }

        // Update event with new available tickets
        evt.AvailableTickets += booking.BookedAmount;
        await _eventService.UpdateEventAsync(evt.Id, evt);

        // Update booking set booking to inactive
        booking.IsActive = false;
        await _bookingRepository.UpdatePartialAsync(booking);
        return booking;
    }
}
