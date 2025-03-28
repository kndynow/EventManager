using EventManager.Api.Models;
using Mapster;
using MongoDB.Bson;

namespace EventManager.Api.Endpoints;

public class GetUserBookings : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/users/{userId}/bookings", Handle).WithSummary("Get all bookings for a user");

    private static async Task<IResult> Handle(string userId, IBookingService bookingService)
    {
        try
        {
            var bookings = await bookingService.GetAllUserBookingsAsync(userId);
            bookings.Adapt<List<BookingResponseDto>>();
            return Results.Ok(bookings);
        }
        catch (KeyNotFoundException)
        {
            return Results.NotFound("No bookings found.");
        }
    }
}
