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
        // The request body containing the updated event data
        [FromBody]
            UpdateEventDto request,
        IEventService eventService
    )
    {
        // Adapt/Maps the request to the Event model
        var eventToUpdate = request.Adapt<Event>();
        try
        {
            // Update the event using the event service
            var updatedEvent = await eventService.UpdateEventAsync(request.Id, eventToUpdate);
            // Return the updated event as a response
            return Results.Ok(updatedEvent.Adapt<EventResponseDto>());
        }
        catch (KeyNotFoundException)
        {
            return Results.NotFound($"Event with id {request.Id} was not found.");
        }
    }
}
