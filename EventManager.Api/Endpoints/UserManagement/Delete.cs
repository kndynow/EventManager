namespace EventManager.Api.Endpoints.UserManagement
{
    public class DeleteUser : IEndpoint
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("/users/{id}", Handle).WithSummary("Delete user");
        }

        public record Request(string Id);

        private static async Task<IResult> Handle(
            [AsParameters] Request request,
            IUserService userService
        )
        {
            try
            {
                await userService.DeleteUserAsync(request.Id);
                return Results.Ok();
            }
            catch (KeyNotFoundException)
            {
                return Results.NotFound($"User with id {request.Id} was not found.");
            }
        }
    }

}
