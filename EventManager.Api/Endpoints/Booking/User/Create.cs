using EventManager.Api.Models;
using EventManager.Core.Models;
using EventManager.Core.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace EventManager.Api.Endpoints;

public class CreateBooking : IEndpoint
{
    // Mapping
    public static void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPost("/users/{userId}/bookings", Handle).WithSummary("Create booking");

    //Logic
    private static async Task<IResult> Handle(
        string userId,
        [FromBody] CreateBookingDto request,
        IBookingService bookingService
    )
    {
        var bookingToCreate = request.Adapt<Booking>();
        bookingToCreate.UserId = userId;

        try
        {
            var createdBooking = await bookingService.CreateBookingAsync(bookingToCreate);

            return TypedResults.Ok(createdBooking.Adapt<BookingResponseDto>());
        }
        catch (KeyNotFoundException)
        {
            return TypedResults.NotFound($"Failed to create booking.");
        }
    }
}
