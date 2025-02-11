using CodeUp.WebApp.Configurations;
using CodeUp.WebApp.Responses;
using CodeUp.WebApp.ViewModels;

namespace CodeUp.WebApp.Services.Authentication;

public sealed class AuthenticationService(IHttpClientFactory httpClientFactory)
                                        : Service, IAuthenticationService
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(WebConfiguration.HTTP_CLIENT_NAME);

    public async Task<Response<string>> LoginAsync(LoginViewModel login)
    {
        var response = await _client.PostAsync("/api/v1/auth/login", GetContent(login)).ConfigureAwait(false);

        if(OpertationIsValid(response))
            return new(null, 400, "Something failed during your login.");

        var result = await DeserializeObjectResponse<Response<LoginViewModel>>(response);

        return result is not null
            ? new("Login successfully!", (int)response.StatusCode, result.Message, result.Errors)
            : new(null, 400, "Something failed during your login.");
    }

    public async Task<Response<string>> RegisterAsync(RegisterViewModel register)
    {
        var response = await _client.PostAsync("/api/v1/auth", GetContent(register)).ConfigureAwait(false);

        if (OpertationIsValid(response))
            return new(null, 400, "Something failed during your login.");

        var result = await DeserializeObjectResponse<Response<LoginViewModel>>(response);

        return result is not null
            ? new("Login successfully!", (int)response.StatusCode, result.Message, result.Errors)
            : new(null, 400, "Something failed during your login.");
    }
}
