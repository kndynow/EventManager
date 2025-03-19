namespace EventManager.Api.Endpoints;

public class GetEvent : IEndpoint
{
    // Mapping
    public static void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/events/{id}", Handle).WithSummary("Get event");

    // Request and Response types
    public record Request(string Id);

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
    private static async Task<IResult> Handle(
        [AsParameters] Request request,
        IEventService eventService
    )
    {
        var ev = await eventService.GetEventByIdAsync(request.Id);

        if (ev is null)
        {
            return Results.NotFound($"Event with id {request.Id} not found.");
        }

        var response = new Response(
            Id: ev.Id,
            Name: ev.Name,
            Description: ev.Description,
            Type: ev.Type,
            Start: ev.StartTime,
            End: ev.EndTime,
            MaxAttendees: ev.MaxAttendees
        );

        return Results.Ok(response);
    }
}
