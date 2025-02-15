using CodeUp.WebApp.Security.Token;
using System.Net.Http.Headers;

namespace CodeUp.WebApp.Security;

public sealed class JwtHandler(ITokenService tokenService) : DelegatingHandler
{
    private readonly ITokenService _tokenService = tokenService;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _tokenService.GetTokenAsync();
        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
