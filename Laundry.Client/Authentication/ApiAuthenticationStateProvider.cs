using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Laundry.Client.Authentication
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _jsRuntime;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public ApiAuthenticationStateProvider(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                // Get token from localStorage
                var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");

                if (string.IsNullOrWhiteSpace(token))
                    return new AuthenticationState(_anonymous);

                var claims = ParseClaimsFromJwt(token);

                var expiry = GetExpiryFromJwt(token);
                if (expiry < DateTime.UtcNow)
                {
                    // Token expired, clear storage and return anonymous
                    await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authToken");
                    return new AuthenticationState(_anonymous);
                }

                var identity = new ClaimsIdentity(claims, "jwt");
                var user = new ClaimsPrincipal(identity);
                return new AuthenticationState(user);
            }
            catch
            {
                return new AuthenticationState(_anonymous);
            }
        }

        public void NotifyUserAuthentication(string token)
        {
            var claims = ParseClaimsFromJwt(token);
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(new AuthenticationState(_anonymous));
            NotifyAuthenticationStateChanged(authState);
        }

        private static DateTime GetExpiryFromJwt(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);
            return token.ValidTo;
        }

        private static Claim[] ParseClaimsFromJwt(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);

            return token.Claims is null
                ? Array.Empty<Claim>()
                : token.Claims.ToArray();
        }
    }
}
