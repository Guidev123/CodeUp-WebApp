using CodeUp.WebApp.Responses;
using CodeUp.WebApp.ViewModels;

namespace CodeUp.WebApp.Services.Authentication;

public interface IAuthenticationService
{
    Task<Response<LoginResponseViewModel>> LoginAsync(LoginViewModel login);
    Task<Response<LoginResponseViewModel>> RegisterAsync(RegisterViewModel register);
    Task<Response<UserViewModel>> GetAsync();
}
