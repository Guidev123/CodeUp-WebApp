namespace CodeUp.WebApp.ViewModels;

public class UserTokenViewModel
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public IEnumerable<ClaimViewModel> Claims { get; set; } = [];
}
