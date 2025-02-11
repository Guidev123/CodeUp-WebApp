using CodeUp.WebApp.Security.Token;
using CodeUp.WebApp.Security;
using CodeUp.WebApp.Services.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;

namespace CodeUp.WebApp.Configurations;

public static class WebConfiguration
{
    public const string HTTP_CLIENT_NAME = "CodeUpAcademy";
    public static string BackendUrl { get; set; } = string.Empty;

    public static void AddDependencies(this IServiceCollection services)
    {
        // SERVICES
        services.AddTransient<IAuthenticationService, AuthenticationService>();

        // SECURITY
        services.AddTransient<ITokenService, TokenService>();
        services.AddScoped<JwtHandler>();
        services.AddAuthorizationCore();
        services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();
        services.AddScoped(x => (IJwtAuthenticationStateProvider)x.GetRequiredService<AuthenticationStateProvider>());

        // TEMPLATE
        services.AddMudServices();

        // HTTP CLIENT
        services.AddHttpClient(HTTP_CLIENT_NAME, options =>
        {
            options.BaseAddress = new Uri(BackendUrl);
        }).AddHttpMessageHandler<JwtHandler>();
    }
}
