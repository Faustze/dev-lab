using System.Text.Json.Serialization;

namespace Cli.Models;

public enum TagColor
{
    [JsonStringEnumMemberName("tag-1")]
    Tag1,

    [JsonStringEnumMemberName("tag-2")]
    Tag2,

    [JsonStringEnumMemberName("tag-3")]
    Tag3,

    [JsonStringEnumMemberName("tag-4")]
    Tag4,

    [JsonStringEnumMemberName("tag-5")]
    Tag5,

    [JsonStringEnumMemberName("tag-6")]
    Tag6,

    [JsonStringEnumMemberName("tag-7")]
    Tag7,
}
