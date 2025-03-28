using EventManager.Api.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace EventManager.Api.Endpoints;

public class UpdateEventPartial : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/events/{id}", Handle).WithSummary("Update event partialy");
    }

    // Handle
    private static async Task<IResult> Handle(
        [FromBody] PatchEventDto request,
        IEventService eventService
    )
    {
        var eventToUpdate = request.Adapt<Event>();
        try
        {
            var updatedEvent = await eventService.UpdateEventPartialAsync(
                request.Id,
                eventToUpdate
            );

            return Results.Ok(updatedEvent.Adapt<EventResponseDto>());
        }
        catch (KeyNotFoundException)
        {
            return Results.NotFound($"Event with id {request.Id} was not found.");
        }
    }
}
