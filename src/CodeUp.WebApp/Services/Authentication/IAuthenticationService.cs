using CodeUp.WebApp.Responses;
using CodeUp.WebApp.ViewModels;

namespace CodeUp.WebApp.Services.Authentication;

public interface IAuthenticationService
{
    Task<Response<string>> LoginAsync(LoginViewModel login);
    Task<Response<string>> RegisterAsync(RegisterViewModel register);
}
