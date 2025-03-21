using EventManager.Api.Models;
using Mapster;
using MongoDB.Bson;

namespace EventManager.Api.Endpoints;

public class GetAllEvents : IEndpoint
{
    // Mapping
    public static void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/events", Handle).WithSummary("Get all events");

    private static async Task<IResult> Handle(IEventService eventService)
    {
        try
        {
            // Fetch all events
            var eventList = await eventService.GetAllEventsAsync();
            // Map the events to the Event model
            eventList.Adapt<List<EventResponseDto>>();
            // Return the events as a response
            return Results.Ok(eventList);
        }
        catch (KeyNotFoundException)
        {
            return Results.NotFound("No events found.");
        }
    }
}
