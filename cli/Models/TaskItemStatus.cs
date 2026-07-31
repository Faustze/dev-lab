using System.Text.Json.Serialization;

namespace Cli.Models;

public enum TaskItemStatus
{
    [JsonStringEnumMemberName("in-progress")]
    InProgress,

    [JsonStringEnumMemberName("done")]
    Done,

    [JsonStringEnumMemberName("canceled")]
    Canceled,
}
