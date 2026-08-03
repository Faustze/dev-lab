using Cli.Models;

namespace Cli.Storage;

interface ITaskStorage
{
    public List<TaskItem> GetAll();
    public TaskItem Add(
        string title,
        TaskItemPriority priority = TaskItemPriority.Middle,
        TaskItemStatus status = TaskItemStatus.InProgress,
        string? description = null
    );
    public TaskItem Done(string id);
    public TaskItem Remove(string id);
}
