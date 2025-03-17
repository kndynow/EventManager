using EventManager.Core.Services;

namespace EventManager.Api.Endpoints.Auth;

public class Login : IEndpoint
{
    // Mapping
    public static void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPost("/auth/login", Handle)
            .WithSummary("Login with username and password")
            .AllowAnonymous();

    // Models
    public record Request(string Username, string Password);

    public record Response(string Username, string Role);

    // Logic
    private static async Task<IResult> Handle(
        Request request,
        IAuthService authService,
        HttpContext context
    )
    {
        var result = await authService.Login(request.Username, request.Password);

        if (result == null)
        {
            return TypedResults.NotFound("Invalid username or password");
        }

        // Set a simple auth cookie
        context.Response.Cookies.Append(
            "auth",
            $"{result.Username}:{result.Role}",
            new CookieOptions
            {
                Secure = true,
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(7),
            }
        );
        var response = new Response(result.Username, result.Role);
        return TypedResults.Ok(response);
    }
}
