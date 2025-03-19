namespace EventManager.Api.Endpoints;

public class CreateEvent : IEndpoint
{
    // Mapping
    public static void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPost("/events", Handle).WithSummary("Create event");

    // Request and Response types
    // Why do we need these? check this video if you are not sure
    // https://youtu.be/xtpPspNdX58?si=GJBUxMzeR2ZJ5Fg_
    public record Request(
        string Name,
        string Description,
        EventType Type,
        DateTime Start,
        DateTime End,
        int MaxAttendees
    );

    public record Response(string id);

    //Logic
    private static async Task<Ok<Response>> Handle(Request request, IEventService eventService)
    {
        // Todo, use a better constructor that enforces setting all necessary properties
        var newEvent = new Event
        {
            Name = request.Name,
            Description = request.Description,
            Type = request.Type,
            StartTime = request.Start,
            EndTime = request.End,
            MaxAttendees = request.MaxAttendees,
        };

        var eventId = await eventService.CreateEventAsync(newEvent);

        return TypedResults.Ok(new Response(eventId));
    }
}
