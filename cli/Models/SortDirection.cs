using System.Text.Json.Serialization;

namespace Cli.Models;

public enum SortDirection
{
    [JsonStringEnumMemberName("asc")]
    Asc,

    [JsonStringEnumMemberName("desc")]
    Desc,
}
