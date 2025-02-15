﻿namespace CodeUp.WebApp.Security.Token;

public interface ITokenService
{
    Task<string?> GetTokenAsync();
    Task SetToken(string token, string refreshToken);
    Task RemoveToken();
}
