namespace EventManager.Api.Endpoints;

public class UpdateEvent : IEndpoint
{
    //Mapping
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/events/{id}", Handle).WithSummary("Update event");
    }

    //Request with id
    public record Request(
        string Id,
        string Name,
        string Description,
        EventType Type,
        DateTime Start,
        DateTime End,
        int MaxAttendees
    );

    public record Response(
        string Id,
        string Name,
        string Description,
        EventType Type,
        DateTime Start,
        DateTime End,
        int MaxAttendees
    );

    //Logics and handling
    private static async Task<IResult> Handle(
        [AsParameters] Request request,
        IEventService eventService
    )
    {
        // Create a new event object with the updated values
        var eventToUpdate = new Event()
        {
            Name = request.Name,
            Description = request.Description,
            Type = request.Type,
            StartTime = request.Start,
            EndTime = request.End,
            MaxAttendees = request.MaxAttendees,
        };
        try
        {
            var updatedEvent = await eventService.UpdateEventAsync(request.Id, eventToUpdate);

            return Results.Ok(MapToResponse(updatedEvent));
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound($"Event with id {request.Id} was not found.");
        }
    }

    private static Response MapToResponse(Event ev) =>
        new(
            Id: ev.Id,
            Name: ev.Name,
            Description: ev.Description,
            Type: ev.Type,
            Start: ev.StartTime,
            End: ev.EndTime,
            MaxAttendees: ev.MaxAttendees
        );
}
