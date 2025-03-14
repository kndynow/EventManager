using EventManager.Core.Services;

namespace EventManager.Api.Endpoints.Auth;

public class Register : IEndpoint
{
    // Mapping
    public static void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPost("/auth/register", Handle).WithSummary("Register a new user").AllowAnonymous();

    // Models
    public record Request(string Username, string Password);

    public record Response(string Username, string Role);

    // Logic
    private static async Task<IResult> Handle(
        Request request,
        IAuthService authService
    )

    {
        var result = await authService.Register(request.Username, request.Password);

        if (result == null)
        {
            return TypedResults.BadRequest("Username already exists");
        }
        var response = new Response(result.Username, result.Role);
        return TypedResults.Ok(response);
    }
}
