using System.Text.Json;
using System.Text.Json.Serialization;

namespace DevLab;

public enum TaskStatus
{
    [JsonStringEnumMemberName("in-progress")]
    InProgress,

    [JsonStringEnumMemberName("done")]
    Done,

    [JsonStringEnumMemberName("canceled")]
    Canceled,
}

public enum TaskPriority
{
    [JsonStringEnumMemberName("low")]
    Low,

    [JsonStringEnumMemberName("middle")]
    Middle,

    [JsonStringEnumMemberName("high")]
    High,
}

public class TaskItem
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public required string Title { get; set; }
    public TaskStatus Status { get; set; } = TaskStatus.InProgress;
    public TaskPriority Priority { get; set; } = TaskPriority.Middle;

    // DateTimeOffset, не DateTime: offset хранится в значении, не зависит от Kind.
    public DateTimeOffset CreatedAtUtc { get; init; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAtUtc { get; set; } = DateTimeOffset.UtcNow;

    // public DateTime SyncAtUtc { get; set; }

    public string? Description { get; set; }

    public Guid? TagId { get; set; }
}

public class TaskTag
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Name { get; set; }

    // Цвет встраивается на клиенте
    public TagColor Color { get; set; }
}

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

public class Program
{
    public static void Main(string[] args)
    {
        var task = new TaskItem
        {
            Id = Guid.NewGuid(),
            Title = "title",
            Status = TaskStatus.InProgress,
            Priority = TaskPriority.Middle,
            CreatedAtUtc = DateTimeOffset.UtcNow,
            UpdatedAtUtc = DateTimeOffset.UtcNow,
            Description = "description",
            TagId = Guid.NewGuid(),
        };
        string json = JsonSerializer.Serialize(task);
        TaskItem restored = JsonSerializer.Deserialize<TaskItem>(json);
        Console.WriteLine($"Serialize: {json}");
        Console.WriteLine($"Deserialize: {restored}");
    }
}
