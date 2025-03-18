using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace EventManager.Client.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;

        public CustomAuthenticationStateProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");

            var identity = string.IsNullOrEmpty(token)
                ? new ClaimsIdentity() // Not authenticated
                : new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt"); // Authenticated

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        public async Task Login(string token)
        {
            await _localStorage.SetItemAsync("authToken", token);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

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
            var jsonBytes = Convert.FromBase64String(payload + new string('=', (4 - payload.Length % 4) % 4));
            var keyValuePairs = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }
    }
}
