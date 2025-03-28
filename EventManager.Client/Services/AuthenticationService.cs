using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace EventManager.Client.Services;

public interface IAuthenticationService
{
    Task<string?> GetUserId();
    Task<string?> GetUserName();
    Task<string?> GetUserRole();
    Task<bool> IsUserAuthenticated();
    Task<ClaimsPrincipal> GetUser();
}

public class AuthenticationService : IAuthenticationService
{
    private readonly AuthenticationStateProvider _authStateProvider;

    public AuthenticationService(AuthenticationStateProvider authStateProvider)
    {
        _authStateProvider = authStateProvider;
    }

    // Gets the user id by checking the authentication state
    public async Task<string?> GetUserId()
    {
        var authState = await _authStateProvider.GetAuthenticationStateAsync();
        return authState.User.FindFirst(c => c.Type == "nameid")?.Value;
    }

    // Gets the user name by checking the authentication state
    public async Task<string?> GetUserName()
    {
        var authState = await _authStateProvider.GetAuthenticationStateAsync();
        return authState.User.FindFirst(c => c.Type == "unique_name")?.Value;
    }

    // Gets the user role by checking the authentication state
    public async Task<string?> GetUserRole()
    {
        var authState = await _authStateProvider.GetAuthenticationStateAsync();
        return authState.User.FindFirst(c => c.Type == "role")?.Value;
    }

    // Checks if the user is authenticated by checking the authentication state which is either true or false
    public async Task<bool> IsUserAuthenticated()
    {
        var authState = await _authStateProvider.GetAuthenticationStateAsync();
        return authState.User.Identity?.IsAuthenticated ?? false;
    }

    // Gets the user by checking the authentication
    public async Task<ClaimsPrincipal> GetUser()
    {
        var authState = await _authStateProvider.GetAuthenticationStateAsync();
        return authState.User;
    }
}
