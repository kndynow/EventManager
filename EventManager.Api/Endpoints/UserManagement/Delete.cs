using EventManager.Api.Models;
using Mapster;

namespace EventManager.Api.Endpoints.UserManagement;

public class Delete : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapDelete("/users/{userId}", Handle).WithSummary("Delete user");

    private static async Task<IResult> Handle(string userId, IUserService userService)
    {
        try
        {
            var deletedUser = await userService.DeleteUserAsync(userId);
            return Results.Ok(deletedUser.Adapt<UserResponseDto>());
        }
        catch (KeyNotFoundException)
        {
            throw new KeyNotFoundException($"User with id {userId} was not found.");
        }
    }
}
