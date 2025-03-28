namespace EventManager.Api.Endpoints;

public class DeleteEvent : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/events/{id}", Handle).WithSummary("Delete event");
    }

    public record Request(string Id);

    private static async Task<IResult> Handle(
        [AsParameters] Request request,
        IEventService eventService
    )
    {
        try
        {
            await eventService.DeleteEventAsync(request.Id);
            return Results.Ok();
        }
        catch (KeyNotFoundException)
        {
            return Results.NotFound($"Event with id {request.Id} was not found.");
        }
    }
}
