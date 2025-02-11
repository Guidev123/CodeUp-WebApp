using CodeUp.WebApp.Security.Token;
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace CodeUp.WebApp.Security;

public class JwtHandler(ITokenService tokenService) : DelegatingHandler
{
    private readonly ITokenService _tokenService = tokenService;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _tokenService.GetToken();
        if (token == null)
            return await base.SendAsync(request, cancellationToken);

        request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
        request.Headers.Add("Bearer", await _tokenService.GetToken());

        return await base.SendAsync(request, cancellationToken);
    }
}
