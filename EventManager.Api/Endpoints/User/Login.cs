using static EventManager.Api.Jwt.TokenService;

namespace EventManager.Api.Endpoints;

public class Login : IEndpoint
{
    // Mapping
    public static void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPost("/auth/login", Handle)
            .WithSummary("Login with username and password")
            .AllowAnonymous();

    // Models
    public record Request(string Username, string Password);

    // Logic
    private static async Task<IResult> Handle(
        Request request,
        IUserService userService,
        ITokenService tokenService
    )
    {
        var user = await userService.Login(request.Username, request.Password);

        if (user is null)
        {
            return TypedResults.NotFound("Invalid username or password");
        }

        var token = tokenService.GenerateToken(user.Username, user.Role);

        return TypedResults.Ok(new { Token = token });
    }
}
