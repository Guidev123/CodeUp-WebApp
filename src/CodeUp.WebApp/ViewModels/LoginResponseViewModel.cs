namespace CodeUp.WebApp.ViewModels;

public class LoginResponseViewModel
{
    public string AccessToken { get; set; } = string.Empty;
    public Guid RefreshToken { get; set; }
    public UserTokenViewModel UserToken { get; set; } = null!;
    public double ExpiresIn { get; set; }
}
