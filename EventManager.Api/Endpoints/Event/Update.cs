using EventManager.Api.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace EventManager.Api.Endpoints;

public class UpdateEvent : IEndpoint
{
    //Mapping
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/events/{id}", Handle).WithSummary("Update event");
    }

    //Logics and handling
    private static async Task<IResult> Handle(
        [FromBody] UpdateEventDto request,
        IEventService eventService
    )
    {
        // Create a new event object with the updated values
        var eventToUpdate = request.Adapt<Event>();
        try
        {
            var updatedEvent = await eventService.UpdateEventAsync(request.Id, eventToUpdate);

            return Results.Ok(updatedEvent.Adapt<EventResponseDto>());
        }
        catch (KeyNotFoundException)
        {
            return Results.NotFound($"Event with id {request.Id} was not found.");
        }
    }
}
