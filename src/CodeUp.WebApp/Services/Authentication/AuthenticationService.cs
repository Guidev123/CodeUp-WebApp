using CodeUp.WebApp.Configurations;
using CodeUp.WebApp.Responses;
using CodeUp.WebApp.ViewModels;

namespace CodeUp.WebApp.Services.Authentication;

public sealed class AuthenticationService(IHttpClientFactory httpClientFactory)
                                        : Service, IAuthenticationService
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(WebConfiguration.HTTP_CLIENT_NAME);

    public async Task<Response<UserViewModel>> GetAsync()
    {
        var response = await _client.GetAsync("/api/v1/auth").ConfigureAwait(false);

        var result = await DeserializeObjectResponse<Response<UserViewModel>>(response);

        return response.IsSuccessStatusCode
            ? new(result!.Data, 200, "Get user successfully") : new(null, 400, "Something failed during your login.", result!.Errors);
    }

    public async Task<Response<LoginResponseViewModel>> LoginAsync(LoginViewModel login)
    {
        var response = await _client.PostAsync("/api/v1/auth/login", GetContent(login)).ConfigureAwait(false);

        var result = await DeserializeObjectResponse<Response<LoginResponseViewModel>>(response);

        return response.IsSuccessStatusCode
            ? new(result!.Data, 201, "Login successfully") : new(null, 400, "Something failed during your login.", result!.Errors);
    }

    public async Task<Response<LoginResponseViewModel>> RegisterAsync(RegisterViewModel register)
    {
        var response = await _client.PostAsync("/api/v1/auth", GetContent(register)).ConfigureAwait(false);

        var result = await DeserializeObjectResponse<Response<LoginResponseViewModel>>(response);

        return response.IsSuccessStatusCode
            ? new(result!.Data, 201, "Login successfully") : new(null, 400, "Something failed during your login.", result!.Errors);
    }
}
