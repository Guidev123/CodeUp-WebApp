using System.Text.Json.Serialization;

namespace CodeUp.WebApp.Responses;

public class Response<TData>
{
    [JsonIgnore]
    public readonly int? StatusCode;
    public const int DEFAULT_SUCCESS_STATUS_CODE = 200;
    public const int DEFAULT_ERROR_STATUS_CODE = 400;

    public Response() { }

    public Response(
        TData? data,
        int? code = DEFAULT_SUCCESS_STATUS_CODE,
        string? message = null,
        List<string>? errors = null)
    {
        StatusCode = code;
        Data = data;
        Message = message;
        Errors = errors;
    }

    public TData? Data { get; set; }
    public string? Message { get; set; }
    public List<string>? Errors { get; set; }
    public bool IsSuccess =>
        StatusCode is >= DEFAULT_SUCCESS_STATUS_CODE and <= 299;
}
