namespace CodeUp.WebApp.Security.Token;

public interface ITokenService
{
    Task<string?> GetToken();
    Task SetToken(string token);
    Task RemoveToken();
}
