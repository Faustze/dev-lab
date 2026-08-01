using System.Text.Json;
using System.Text.Json.Serialization;

public static class JsonOptionsProvider
{
    public static readonly JsonSerializerOptions Options = new()
    {
        Converters = { new JsonStringEnumConverter() },
    };
}
