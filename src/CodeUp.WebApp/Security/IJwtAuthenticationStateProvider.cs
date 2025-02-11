using Microsoft.AspNetCore.Components.Authorization;

namespace CodeUp.WebApp.Security;

public interface IJwtAuthenticationStateProvider
{
    Task<bool> CheckAuthenticatedAsync();
    Task<AuthenticationState> GetAuthenticationStateAsync();
    Task SetTokenAsync(string token);
    Task RemoveTokenAsync();
    void NotifyAuthenticationStateChanged();
}
