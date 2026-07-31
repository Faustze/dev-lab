using System.Text.Json.Serialization;

namespace Cli.Models;

public enum TaskItemPriority
{
    [JsonStringEnumMemberName("low")]
    Low,

    [JsonStringEnumMemberName("middle")]
    Middle,

    [JsonStringEnumMemberName("high")]
    High,
}
