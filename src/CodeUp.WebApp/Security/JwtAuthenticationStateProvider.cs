using CodeUp.WebApp.Configurations;
using CodeUp.WebApp.Responses;
using CodeUp.WebApp.Security.Token;
using CodeUp.WebApp.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace CodeUp.WebApp.Security;

public class JwtAuthenticationStateProvider(IHttpClientFactory httpClientFactory, ITokenService tokenService)
    : AuthenticationStateProvider, IJwtAuthenticationStateProvider
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(WebConfiguration.HTTP_CLIENT_NAME);
    private readonly ITokenService _tokenService = tokenService;
    private ClaimsPrincipal _currentUser = new(new ClaimsIdentity());
    private bool _isAuthenticated = false;

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        _isAuthenticated = false;
        try
        {
            var token = await _tokenService.GetToken();
            if (string.IsNullOrEmpty(token))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync("/api/v1/auth/manage/info");

            if (!response.IsSuccessStatusCode)
            {
                await RemoveTokenAsync();
                _isAuthenticated = false;
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var content = await response.Content.ReadAsStringAsync();
            var userInfo = JsonSerializer.Deserialize<Response<UserViewModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (userInfo is null)
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, userInfo.Data!.Email),
                new(ClaimTypes.NameIdentifier, userInfo.Data.Id.ToString())
            };

            foreach (var role in userInfo.Data.Claims)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Value));
            }

            var identity = new ClaimsIdentity(claims, "jwt");
            _currentUser = new ClaimsPrincipal(identity);

            _isAuthenticated = true;
            return new AuthenticationState(_currentUser);
        }
        catch
        {
            _isAuthenticated = false;
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
    }

    public async Task<bool> CheckAuthenticatedAsync()
    {
        await GetAuthenticationStateAsync();
        return _isAuthenticated;
    }

    public async Task SetTokenAsync(string token)
    {
        await _tokenService.SetToken(token);
        NotifyAuthenticationStateChanged();
    }

    public async Task RemoveTokenAsync()
    {
        await _tokenService.RemoveToken();
        NotifyAuthenticationStateChanged();
    }

    public void NotifyAuthenticationStateChanged()
        => NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
}
