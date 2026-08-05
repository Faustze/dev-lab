using Cli.Models;

namespace Cli.Utils;

public static class SortBy
{
    public static List<TaskItem> SortByPriority(IEnumerable<TaskItem> source, SortDirection dir)
    {
        return dir == SortDirection.Desc
            ? source.OrderByDescending(t => t.Priority).ToList()
            : source.OrderBy(t => t.Priority).ToList();
    }
}
