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
        var options = JsonOptionsProvider.Options;
        string json1 =
            """{"Status": "in-progress", "CreatedAtUtc": "2026-08-01T16:48:33+00:00", "UpdatedAtUtc": "2026-08-01T16:48:33+00:00", "Description": "description", "TagId": "9e69d77a-7b24-4934-bbae-ac185286eda0"}""";
        string json2 =
            """{"Title": "title", "CreatedAtUtc": "2026-08-01T16:48:33+00:00", "UpdatedAtUtc": "2026-08-01T16:48:33+00:00", "Description": "description", "TagId": "9e69d77a-7b24-4934-bbae-ac185286eda0"}""";
        string json3 =
            """{"Title": "title", "Status": "unknown" , "CreatedAtUtc": "2026-08-01T16:48:33+00:00", "UpdatedAtUtc": "2026-08-01T16:48:33+00:00", "Description": "description", "TagId": "9e69d77a-7b24-4934-bbae-ac185286eda0"}""";
        string json4 =
            """{"Id": "9e69d77a-7b24-4934-bbae-ac185286eda0" ,"Title": "title", "Status": "done", "CreatedAtUtc": "2026-08-01T16:48:33+00:00", "UpdatedAtUtc": "2026-08-01T16:48:33+00:00", "Description": "description", "TagId": "9e69d77a-7b24-4934-bbae-ac185286eda0"}""";
        string json5 =
            """{"Title": "title", "Status": "done", "CreatedAtUtc": "2026-08-01T16:48:33+00:00", "UpdatedAtUtc": "2026-08-01T16:48:33+00:00", "Description": null, "TagId": "9e69d77a-7b24-4934-bbae-ac185286eda0"}""";
        string json6 =
            """{"Title": "title", "Status": "done", "CreatedAtUtc": "2026-07-01", "UpdatedAtUtc": "2026-07-01", "Description": null, "TagId": "9e69d77a-7b24-4934-bbae-ac185286eda0"}""";
        var samples = new[]
        {
            ("json1", json1),
            ("json2", json2),
            ("json3", json3),
            ("json4", json4),
            ("json5", json5),
            ("json6", json6),
        };

        foreach (var (name, json) in samples)
        {
            try
            {
                Console.WriteLine(JsonSerializer.Deserialize<TaskItem>(json, options));
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error {name}: {ex.Message}");
            }
        }
    }
}
