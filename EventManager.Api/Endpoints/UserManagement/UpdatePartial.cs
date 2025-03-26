using EventManager.Api.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace EventManager.Api.Endpoints;

public class UpdateUserPartial : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/users/{id}", Handle).WithSummary("Update user partialy");
    }

    // Handle
    private static async Task<IResult> Handle(
        [FromBody] PatchUserDto request,
        IUserService userService
    )
    {
        var userToUpdate = request.Adapt<User>();
        try
        {
            var updatedUser = await userService.UpdateUserPartialAsync(
                request.Id,
                userToUpdate
            );

            return Results.Ok(updatedUser.Adapt<UserResponseDto>());
        }
        catch (KeyNotFoundException)
        {
            return Results.NotFound($"User with id {request.Id} was not found.");
        }
    }
}
