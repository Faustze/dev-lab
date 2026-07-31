namespace Cli.Models;

public class TaskTag
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Name { get; set; }

    // Цвет встраивается на клиенте
    public TagColor Color { get; set; }
}
