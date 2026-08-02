using Cli.Models;

namespace Cli;

public class Program
{
    static TaskItem CreateTaskItem(
        string title,
        TaskItemPriority priority = TaskItemPriority.Middle,
        TaskItemStatus status = TaskItemStatus.InProgress,
        string? description = null
    )
    {
        return new TaskItem
        {
            Title = title,
            Status = status,
            Priority = priority,
            Description = description,
        };
    }

    static List<TaskItem> CreateSampleTasks() =>
        new()
        {
            CreateTaskItem(
                "Todo1",
                TaskItemPriority.High,
                TaskItemStatus.InProgress,
                "description1"
            ),
            CreateTaskItem("Todo2", TaskItemPriority.Middle, TaskItemStatus.Done, "description2"),
            CreateTaskItem("Todo3", TaskItemPriority.Low, TaskItemStatus.Canceled, "description3"),
        };

    static void ShowTasks(List<TaskItem> tasks)
    {
        foreach (TaskItem t in tasks)
            Console.WriteLine(t);

        Console.WriteLine($"Tasks count: {tasks.Count}");
    }

    public static int Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.Error.WriteLine(
                "Arguments error... Usage: add <title> <--priority=priority(high, middle, low)> <--status=status(in-progress, done, canceled)> <desc>"
            );
            return 1;
        }
        switch (args[0])
        {
            case "add":
            {
                if (args.Length < 2)
                {
                    Console.Error.WriteLine(
                        "Arguments error... Usage: add <title> <--priority=priority(high, middle, low)> <--status=status(in-progress, done, canceled)> <desc>"
                    );
                    return 1;
                }

                string title = args[1];
                TaskItemPriority priority = TaskItemPriority.Middle;
                TaskItemStatus status = TaskItemStatus.InProgress;
                string? description = null;

                for (int i = 2; i < args.Length; i++)
                {
                    if (args[i].StartsWith("--priority"))
                    {
                        string[] priorityArgs = args[i].Split("=");
                        string rawPriority = priorityArgs[1];
                        if (!Enum.TryParse(rawPriority, true, out priority))
                        {
                            Console.Error.WriteLine(
                                $"Wrong priority value: {Enum.GetNames(typeof(TaskItemPriority))}"
                            );
                            return 1;
                        }
                    }
                    else if (args[i].StartsWith("--status"))
                    {
                        string[] statusArgs = args[i].Split("=");
                        string rawStatus = statusArgs[1].Replace("-", "");
                        if (!Enum.TryParse(rawStatus, true, out status))
                        {
                            Console.Error.WriteLine(
                                $"Wrong status value: {Enum.GetNames(typeof(TaskItemStatus))}"
                            );
                            return 1;
                        }
                    }
                    else
                    {
                        description = args[i];
                    }
                }
                TaskItem t = CreateTaskItem(title, priority, status, description);
                Console.WriteLine(t);
                return 0;
            }

            case "list":
                ShowTasks(CreateSampleTasks());
                return 0;

            default:
                Console.Error.WriteLine($"Unknown command : {args[0]}");
                return 1;
        }
    }
}
