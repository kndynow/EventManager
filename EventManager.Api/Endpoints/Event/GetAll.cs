using Microsoft.AspNetCore.Authorization;

namespace EventManager.Api.Endpoints;

public class GetAllEvents : IEndpoint
{
    // Mapping

    public static void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/events", Handle).WithSummary("Get all events");
        

    // Request and Response types
    public record Response(
        string Id,
        string Name,
        string Description,
        EventType Type,
        DateTime Start,
        DateTime End,
        int MaxAttendees
    );

    //Logic
    private static async Task<IEnumerable<Response>> Handle(IEventService eventService)
    {
        var events = await eventService.GetAllEventsAsync();

        return events.Select(item => new Response(
                Id: item.Id,
                Name: item.Name,
                Description: item.Description,
                Type: item.Type,
                Start: item.StartTime,
                End: item.EndTime,
                MaxAttendees: item.MaxAttendees
            ))
            .ToList();
    }
}
