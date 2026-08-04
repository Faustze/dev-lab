using System.Text.Json;
using Cli.Models;

namespace Cli.Storage;

public class TaskMemoryStorage : ITaskStorage
{
    private readonly List<TaskItem> _tasks = new();
    private readonly string _filePath;

    public TaskMemoryStorage(string filePath)
    {
        _filePath = filePath;
    }

    public List<TaskItem> GetAll()
    {
        return _tasks.ToList();
    }

    public List<TaskItem> GetAllFiltered(TaskItemStatus status)
    {
        return _tasks.Where(t => t.Status == status).ToList();
    }

    public TaskItem? GetById(Guid id)
    {
        return _tasks.FirstOrDefault(t => t.Id == id);
    }

    public (TaskItem? task, int matchCount) GetByIdPrefix(string idPrefix)
    {
        var matches = _tasks.Where(t => t.Id.ToString().StartsWith(idPrefix, StringComparison.OrdinalIgnoreCase)).ToList();
        return (matches.Count == 1 ? matches[0] : null, matches.Count);
    }

    public void LoadFromJson(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
            return;

        var tasks = JsonSerializer.Deserialize<List<TaskItem>>(json, JsonOptionsProvider.Options);

        if (tasks is null)
            return;

        foreach (var task in tasks)
        {
            if (!_tasks.Any(t => t.Id == task.Id))
                _tasks.Add(task);
        }
    }

    public TaskItem Add(
        string title,
        TaskItemPriority priority = TaskItemPriority.Middle,
        string? description = null
    )
    {
        TaskItem newTask = new TaskItem
        {
            Title = title,
            Status = TaskItemStatus.InProgress,
            Priority = priority,
            Description = description,
        };
        _tasks.Add(newTask);
        return newTask;
    }

    public TaskItem Done(string id)
    {
        (TaskItem? task, int count) = GetByIdPrefix(id);
        if (count == 0)
            throw new InvalidOperationException($"Task '{id}' not found.");
        if (task is null)
            throw new InvalidOperationException($"Ambiguous identifier '{id}' — matches {count} tasks.");
        task.Status = TaskItemStatus.Done;
        task.UpdatedAtUtc = DateTimeOffset.UtcNow;
        return task;
    }

    public TaskItem Remove(string id)
    {
        (TaskItem? task, int count) = GetByIdPrefix(id);
        if (count == 0)
            throw new InvalidOperationException($"Task '{id}' not found.");
        if (task is null)
            throw new InvalidOperationException($"Ambiguous identifier '{id}' — matches {count} tasks.");
        _tasks.RemoveAll(t => t.Id == task.Id);
        return task;
    }

    public void Save()
    {
        var options = new JsonSerializerOptions(JsonOptionsProvider.Options)
        {
            WriteIndented = true
        };
        string json = JsonSerializer.Serialize(_tasks, options);
        File.WriteAllText(_filePath, json);
    }
}
