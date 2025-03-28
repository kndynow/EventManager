namespace EventManager.Api.Endpoints;

public class Register : IEndpoint
{
    // Mapping
    public static void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPost("/auth/register", Handle).WithSummary("Register a new user").AllowAnonymous();

    // Models
    public record Request(string Username, string Password);

    public record Response(string Username, string Role);

    // Logic
    private static async Task<IResult> Handle(Request request, IUserService userService)
    {
        var result = await userService.Register(request.Username, request.Password);

        if (result is null)
        {
            return TypedResults.BadRequest(
                "Invalid username and password or the username already exists."
            );
        }
        var response = new Response(result.Username, result.Role);
        return TypedResults.Ok(response);
    }
}
