using EventManager.Core.Models;
using MongoDB.Driver;

namespace EventManager.Core.Data;

public interface IBookingRepository
{
    Task CreateAsync(Booking newBooking);
    Task<Booking> GetByIdAsync(string id);
    Task<IEnumerable<Booking>> GetAllAsync();
    Task UpdatePartialAsync(Booking updatedBooking);
    Task DeleteAsync(string id);
}

public class BookingRepository : IBookingRepository
{
    // Handles interactions with the MongoDB database for event-related data.
    // Connects to MongoDB server via MongoDbContext.
    private readonly IMongoCollection<Booking> _bookings;

    // Constructor
    public BookingRepository(MongoDbContext context)
    {
        _bookings = context.Bookings;
    }

    // Reusable filter to get event by id
    private static FilterDefinition<Booking> GetBookingIdFilter(string id) =>
        Builders<Booking>.Filter.Eq(b => b.Id, id);

    public async Task CreateAsync(Booking newBooking)
    {
        await _bookings.InsertOneAsync(newBooking);
    }

    public async Task<IEnumerable<Booking>> GetAllAsync()
    {
        var filter = Builders<Booking>.Filter.Empty;
        return await _bookings.Find(filter).ToListAsync();
    }

    public async Task<Booking> GetByIdAsync(string id)
    {
        var filter = GetBookingIdFilter(id);
        return await _bookings.Find(filter).FirstOrDefaultAsync();
    }

    public async Task UpdatePartialAsync(Booking updatedBooking)
    {
        var bookingFilter = GetBookingIdFilter(updatedBooking.Id);
        var updateBookings = Builders<Booking>
            .Update.Set(b => b.BookedAmount, updatedBooking.BookedAmount)
            .Set(b => b.IsActive, updatedBooking.IsActive);

        await _bookings.UpdateOneAsync(bookingFilter, updateBookings);
    }

    public Task DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }
}
