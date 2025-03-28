using EventManager.Api.Models;
using Mapster;

namespace EventManager.Api.Endpoints;

public class GetEvent : IEndpoint
{
    // Mapping
    public static void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/events/{id}", Handle).WithSummary("Get event");

    public record Request(string Id);

    //Logic
    private static async Task<IResult> Handle(
        [AsParameters] Request request,
        IEventService eventService
    )
    {
        try
        {
            var ev = await eventService.GetEventByIdAsync(request.Id);
            return Results.Ok(ev);
        }
        catch (KeyNotFoundException)
        {
            return Results.NotFound($"Event with id {request.Id} was not found.");
        }
    }
}
