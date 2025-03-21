using EventManager.Api.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace EventManager.Api.Endpoints;

public class CreateEvent : IEndpoint
{
    // Mapping
    public static void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPost("/events", Handle).WithSummary("Create event");

    //Logic
    private static async Task<IResult> Handle(
        [FromBody] CreateEventDto request,
        IEventService eventService
    )
    {
        var eventToCreate = request.Adapt<Event>();

        try
        {
            var createdEvent = await eventService.CreateEventAsync(eventToCreate);

            return TypedResults.Ok(createdEvent.Adapt<EventResponseDto>());
        }
        catch (KeyNotFoundException)
        {
            return TypedResults.NotFound($"Failed to create event.");
        }
    }
}
