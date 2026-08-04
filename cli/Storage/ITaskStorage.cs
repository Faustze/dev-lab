using Cli.Models;

namespace Cli.Storage;

interface ITaskStorage
{
    public List<TaskItem> GetAll();
    public TaskItem Add(
        string title,
        TaskItemPriority priority = TaskItemPriority.Middle,
        string? description = null
    );
    public TaskItem Done(string id);
    public TaskItem Remove(string id);
    public void Save();
}
