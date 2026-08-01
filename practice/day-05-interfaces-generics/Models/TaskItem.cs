namespace Cli.Models;

public class TaskItem
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public required string Title { get; set; }
    public TaskItemStatus Status { get; set; } = TaskItemStatus.InProgress;
    public TaskItemPriority Priority { get; set; } = TaskItemPriority.Middle;

    // DateTimeOffset, не DateTime: offset хранится в значении, не зависит от Kind.
    public DateTimeOffset CreatedAtUtc { get; init; }
    public DateTimeOffset UpdatedAtUtc { get; set; }

    // public DateTime SyncAtUtc { get; set; }

    public string? Description { get; set; }

    public Guid? TagId { get; set; }

    public override string ToString()
    {
        return $"[{Status}] {Title} ({Priority}, {Id} crt={CreatedAtUtc} upd={UpdatedAtUtc})";
    }

    public TaskItem()
    {
        DateTimeOffset utcNow = DateTimeOffset.UtcNow;
        CreatedAtUtc = utcNow;
        UpdatedAtUtc = utcNow;
    }
}
