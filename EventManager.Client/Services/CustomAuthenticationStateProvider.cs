using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace EventManager.Client.Services
{
    //Manages the authentication
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        //LocalStorage is used to store and retrieve JWT:s
        private readonly ILocalStorageService _localStorage;

        public CustomAuthenticationStateProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        //Retrieves JWT from localstorage.
        //If no token exists it creates an empty ClaimsIdentity(user not logged in)
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");

            var identity = string.IsNullOrEmpty(token)
                ? new ClaimsIdentity() // Not authenticated
                : new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt"); // Authenticated

            var roleClaim = identity.FindFirst("role");
            if (roleClaim != null)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, roleClaim.Value)); // Explicitly setting it as a role
            }

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        //Saves the provided token in localstorage.
        //Notifies authentication system that user's authenticationstate has changed.
        public async Task Login(string token)
        {
            await _localStorage.RemoveItemAsync("authToken");
            await _localStorage.SetItemAsync("authToken", token);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        //Removes user token from local storage
        //Notifies authentication system that user isn't authenticated anymore
        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            var emptyUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(emptyUser)));
        }

        //Lets the AuthenticationStateProvider extract user data from the JWT
        private IEnumerable<Claim> ParseClaimsFromJwt(string token)
        {
            var payload = token.Split('.')[1];
            var jsonBytes = Convert.FromBase64String(
                payload + new string('=', (4 - payload.Length % 4) % 4)
            );
            var keyValuePairs = System.Text.Json.JsonSerializer.Deserialize<
                Dictionary<string, object>
            >(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }
    }
}
