using Microsoft.JSInterop;

namespace CodeUp.WebApp.Security.Token;

public class TokenService(IJSRuntime jsRuntime) : ITokenService
{
    private readonly IJSRuntime _jsRuntime = jsRuntime;
    private const string TOKEN_KEY = "accessToken";
    private const string REFRESH_TOKEN = "refreshToken";
    public async Task<string?> GetTokenAsync()
        => await _jsRuntime.InvokeAsync<string>("localStorage.getItem", TOKEN_KEY);

    public async Task SetToken(string token, string refreshToken)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", TOKEN_KEY, token);
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", REFRESH_TOKEN, refreshToken);
    }

    public async Task RemoveToken()
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", TOKEN_KEY);
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", REFRESH_TOKEN);
    }
}
