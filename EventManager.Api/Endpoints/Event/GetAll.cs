using EventManager.Api.Models;
using Mapster;

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
            var ev = await eventService.GetAllEventsAsync();
            return Results.Ok(ev);
        }
        catch (KeyNotFoundException)
        {
            return Results.NotFound("No events found.");
        }
    }
}
