using System;
using EventManager.Api.Models;
using Mapster;

namespace EventManager.Api.Endpoints;

public class CancelBooking : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPatch("/users/{userId}/bookings/{bookingId}/cancel", Handle)
            .WithSummary("Cancel booking");

    private static async Task<IResult> Handle(
        string userId,
        string bookingId,
        IBookingService bookingService
    )
    {
        try
        {
            var cancelledBooking = await bookingService.CancelBookingAsync(bookingId);
            if (cancelledBooking.UserId != userId)
            {
                return TypedResults.Forbid();
            }

            return TypedResults.Ok(cancelledBooking.Adapt<BookingResponseDto>());
        }
        catch (KeyNotFoundException ex)
        {
            return TypedResults.NotFound(ex.Message);
        }
    }
}
