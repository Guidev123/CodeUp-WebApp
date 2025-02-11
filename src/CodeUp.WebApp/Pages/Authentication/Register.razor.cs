using CodeUp.WebApp.Services.Authentication;
using CodeUp.WebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace CodeUp.WebApp.Pages.Authentication;

public partial class RegisterPage : ComponentBase
{
    [Inject]
    public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    [Inject]
    public ISnackbar Snackbar { get; set; } = default!;

    [Inject]
    public IAuthenticationService UserService { get; set; } = default!;

    public RegisterViewModel InputModel { get; set; } = new();

    public bool IsBusy { get; set; } = false;

    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity is not null && user.Identity.IsAuthenticated)
            NavigationManager.NavigateTo("/");
    }

    protected async Task OnValidSubmitAsync()
    {
        IsBusy = true;

        try
        {
            var result = await UserService.RegisterAsync(InputModel);
            if (result.IsSuccess)
                NavigationManager.NavigateTo("/");
            else
            {
                foreach (var item in result.Errors!)
                    Snackbar.Add(item, Severity.Error);
            }
        }
        catch
        {
            Snackbar.Add("Something has failed", Severity.Error);
            throw;
        }
        finally
        {
            IsBusy = false;
        }
    }
}
