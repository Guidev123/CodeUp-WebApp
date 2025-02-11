namespace CodeUp.WebApp.ViewModels;

public class UserViewModel
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public IReadOnlyDictionary<string, string> Claims { get; set; } = default!;
}
