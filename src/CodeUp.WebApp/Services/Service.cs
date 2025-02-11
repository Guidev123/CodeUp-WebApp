using System.Net;
using System.Text.Json;
using System.Text;

namespace CodeUp.WebApp.Services;

public abstract class Service
{
    protected StringContent GetContent(object data)
        => new(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

    protected async Task<T?> DeserializeObjectResponse<T>(HttpResponseMessage response)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            IncludeFields = true
        };

        var json = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<T>(json, options);
    }

    protected static bool OpertationIsValid(HttpResponseMessage response)
        => response.EnsureSuccessStatusCode().IsSuccessStatusCode;

}
