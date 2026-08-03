using Cli.Models;

namespace Cli.Storage;

public class TaskMemoryStorage : ITaskStorage
{
    private static List<TaskItem> _tasks = new();

    public List<TaskItem> GetAll()
    {
        return _tasks.ToList();
    }

    public TaskItem Add(
        string title,
        TaskItemPriority priority = TaskItemPriority.Middle,
        TaskItemStatus status = TaskItemStatus.InProgress,
        string? description = null
    )
    {
        TaskItem newTask = new TaskItem
        {
            Title = title,
            Status = status,
            Priority = priority,
            Description = description,
        };
        _tasks.Add(newTask);
        return newTask;
    }

    public TaskItem Done(string id)
    {
        bool guidParsed = Guid.TryParse(id, out Guid guid);
        if (!guidParsed)
            throw new Exception($"{id} parsing error.");
        TaskItem? task = GetAll().SingleOrDefault(t => t.Id == guid);
        if (task is null)
            throw new NullReferenceException($"id={guid} is not found.");
        task.Status = TaskItemStatus.Done;
        return task;
    }

    public TaskItem Remove(string id)
    {
        bool guidParsed = Guid.TryParse(id, out Guid guid);
        if (!guidParsed)
            throw new Exception($"{id} parsing error.");
        TaskItem? task = _tasks.SingleOrDefault(t => t.Id == guid);
        if (task is null)
            throw new NullReferenceException($"id={guid} is not found.");
        _tasks.RemoveAll(t => t.Id == guid);
        return task;
    }
}
