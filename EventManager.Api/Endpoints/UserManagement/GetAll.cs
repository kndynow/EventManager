using EventManager.Api.Models;
using Mapster;
using MongoDB.Bson;

namespace EventManager.Api.Endpoints;

public class GetAllUsers : IEndpoint
{
    // Mapping
    public static void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/users", Handle).WithSummary("Get all users");

    private static async Task<IResult> Handle(IUserService userService)
    {
        try
        {
            // Fetch all events
            var userList = await userService.GetAllUsersAsync();
            // Map the events to the Event model
            userList.Adapt<List<UserResponseDto>>();
            // Return the events as a response
            return Results.Ok(userList);
        }
        catch (KeyNotFoundException)
        {
            return Results.NotFound("No events found.");
        }
    }
}
