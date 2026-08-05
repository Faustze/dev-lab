using System.Text.Json;
using Cli.Models;
using Cli.Storage;

namespace Cli;

public class Program
{
    private static readonly string MockFilePath = "Mocks/_tasks.json";
    private static TaskMemoryStorage _storage = null!;

    static bool EnsureMockTasksFileExists()
    {
        _storage = new TaskMemoryStorage(MockFilePath);

        string json;
        try
        {
            json = File.ReadAllText(MockFilePath);
            _storage.LoadFromJson(json);
            return true;
        }
        catch (FileNotFoundException ex)
        {
            Console.Error.WriteLine($"Json file {ex.FileName} not found. Recreated.");
            CreateMockTasksJsonFile();
            json = File.ReadAllText(MockFilePath);
            _storage.LoadFromJson(json);
            return true;
        }
        catch (JsonException)
        {
            Console.Error.WriteLine(
                $"Json file is damaged. Correct it here: '{MockFilePath}' and launch again."
            );
            return false;
        }
    }

    static void CreateMockTasksJsonFile()
    {
        var tasks = new List<TaskItem>
        {
            new TaskItem
            {
                Title = "Setup project structure",
                Priority = TaskItemPriority.High,
                Description = "Initialize CLI project with Models, Storage, and Program",
            },
            new TaskItem
            {
                Title = "Implement JSON storage",
                Priority = TaskItemPriority.High,
                Description = "Add file-based persistence for tasks",
            },
            new TaskItem
            {
                Title = "Add command parsing",
                Priority = TaskItemPriority.Low,
                Status = TaskItemStatus.Done,
                Description = "Parse add/list/done/remove commands with flags",
            },
        };

        var options = new JsonSerializerOptions(JsonOptionsProvider.Options)
        {
            WriteIndented = true,
        };
        File.WriteAllText(MockFilePath, JsonSerializer.Serialize(tasks, options));
    }

    static void ShowTasks(List<TaskItem> tasks)
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks.");
            return;
        }

        const int idLen = 8;
        Console.WriteLine($"{"ID", -8}  {"Title", -30}  {"Priority"}");
        Console.WriteLine(new string('-', idLen + 2 + 30 + 2 + 10));
        foreach (TaskItem t in tasks)
            Console.WriteLine($"{t.Id.ToString()[..idLen], -8}  {t.Title, -30}  {t.Priority}");
    }

    static bool IsValidIdArg(string arg)
    {
        return !arg.StartsWith("--");
    }

    public static int Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.Error.WriteLine(
                "Usage: cli <command> [args]\n\nCommands:\n  add <title> [--priority=low|middle|high]\n  list [--all]\n  done <id>\n  rm <id>"
            );
            return 1;
        }

        if (!EnsureMockTasksFileExists())
            return 1;

        switch (args[0])
        {
            case "add":
            {
                if (args.Length < 2)
                {
                    Console.Error.WriteLine("Error: title is required.");
                    return 1;
                }

                string title = args[1];
                TaskItemPriority priority = TaskItemPriority.Middle;

                for (int i = 2; i < args.Length; i++)
                {
                    if (args[i].StartsWith("--priority="))
                    {
                        string rawPriority = args[i].Split('=')[1];
                        if (!Enum.TryParse<TaskItemPriority>(rawPriority, true, out priority))
                        {
                            Console.Error.WriteLine(
                                $"Invalid priority: {rawPriority}. Valid values: low, middle, high"
                            );
                            return 1;
                        }
                    }
                    else
                    {
                        Console.Error.WriteLine($"Unknown flag: {args[i]}");
                        return 1;
                    }
                }

                TaskItem t = _storage.Add(title, priority);
                _storage.Save();
                Console.WriteLine(t);
                return 0;
            }

            case "list":
            {
                bool all = args.Length > 1 && args[1] == "--all";
                List<TaskItem> tasks = all
                    ? _storage.GetAll()
                    : _storage.GetAllFiltered(TaskItemStatus.InProgress);
                ShowTasks(tasks);
                return 0;
            }

            case "done":
            {
                if (args.Length < 2)
                {
                    Console.Error.WriteLine("Error: task ID is required.");
                    return 1;
                }

                if (!IsValidIdArg(args[1]))
                {
                    Console.Error.WriteLine($"Error: expected task ID, got flag '{args[1]}'.");
                    return 1;
                }

                try
                {
                    TaskItem t = _storage.Done(args[1]);
                    _storage.Save();
                    Console.WriteLine($"Done: {t.Title} ({t.Id})");
                    return 0;
                }
                catch (InvalidOperationException ex)
                {
                    Console.Error.WriteLine(ex.Message);
                    return 1;
                }
            }

            case "rm":
            case "remove":
            {
                if (args.Length < 2)
                {
                    Console.Error.WriteLine("Error: task ID is required.");
                    return 1;
                }

                if (!IsValidIdArg(args[1]))
                {
                    Console.Error.WriteLine($"Error: expected task ID, got flag '{args[1]}'.");
                    return 1;
                }

                try
                {
                    TaskItem t = _storage.Remove(args[1]);
                    _storage.Save();
                    Console.WriteLine($"Removed: {t.Title} ({t.Id})");
                    return 0;
                }
                catch (InvalidOperationException ex)
                {
                    Console.Error.WriteLine(ex.Message);
                    return 1;
                }
            }

            default:
                Console.Error.WriteLine($"Unknown command: {args[0]}");
                return 1;
        }
    }
}
