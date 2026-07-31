using System.Text.Json;
using Cli.Models;

namespace Cli;

public class Program
{
    public static void Main(string[] args)
    {
        static TaskItem CreateTaskItem(
            string title,
            TaskItemPriority priority = TaskItemPriority.Middle,
            string? description = null
        )
        {
            return new TaskItem
            {
                Title = title,
                Priority = priority,
                Description = description,
            };
        }
        var task1 = CreateTaskItem("Todo1", TaskItemPriority.High, "description1");
        var task2 = CreateTaskItem("Todo2", TaskItemPriority.Middle, "description2");
        var task3 = CreateTaskItem("Todo3", TaskItemPriority.Low, "description3");
        List<TaskItem> tasks = new List<TaskItem> { task1, task2, task3 };
        foreach (TaskItem t in tasks)
        {
            Console.WriteLine(t);
        }
        Console.WriteLine($"Tasks count: {tasks.Count}");
    }
}
