using Microsoft.JSInterop;

namespace CodeUp.WebApp.Security.Token;

public class TokenService(IJSRuntime jsRuntime) : ITokenService
{
    private readonly IJSRuntime _jsRuntime = jsRuntime;
    private const string TOKEN_KEY = "authToken";
    public async Task<string?> GetTokenAsync()
        => await _jsRuntime.InvokeAsync<string>("localStorage.getItem", TOKEN_KEY);

    public async Task SetToken(string token)
        => await _jsRuntime.InvokeVoidAsync("localStorage.setItem", TOKEN_KEY, token);

    public async Task RemoveToken()
        => await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", TOKEN_KEY);
}
