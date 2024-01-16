using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace MediaLibrary.WebApp.Client.Auth
{
    public class AuthenticationProviderTest : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var anonimous = new ClaimsIdentity();
            var adminUser = new ClaimsIdentity(new List<Claim>
            {
                new Claim("FirstName", "Dario"),
                new Claim(ClaimTypes.Name, "josedarioalzate@gmail.com"),
                new Claim(ClaimTypes.Role, "Admin")
            },authenticationType: "test");

            var user = new ClaimsIdentity(new List<Claim>
            {
                new Claim("FirstName", "ComoTaMuchacho"),
                new Claim(ClaimTypes.Name, "parbolitas@gmail.com"),
                new Claim(ClaimTypes.Role, "User")
            }, authenticationType: "test");

            // Prueba con el usuario respectivo
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonimous)));
        }
    }
}
