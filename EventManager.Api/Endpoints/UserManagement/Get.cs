using EventManager.Api.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace EventManager.Api.Endpoints.UserManagement;
public class GetUser : IEndpoint
{
    // Mapping
    public static void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/users/{id}", Handle).WithSummary("Get user");

    public record Request(string Id);

    //Logic
    private static async Task<IResult> Handle(
        [AsParameters] Request request,
        IUserService userService
    )
    {
        try
        {
            var ev = await userService.GetUserByIdAsync(request.Id);
            return Results.Ok(ev);
        }
        catch (KeyNotFoundException)
        {
            return Results.NotFound($"User with id {request.Id} was not found.");
        }
    }
}