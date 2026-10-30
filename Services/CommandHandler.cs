using System;
using TaskTrackerCS.Services;
using TaskTrackerCS.Repositories;
using TaskTrackerCS.Models;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace TaskTrackerCS
{
    public static class CommandHandler
    {
        private readonly static TaskHandler _th = new TaskHandler(new TaskRepository());

        private static readonly Dictionary<string, EStatusTask> _filters = new(StringComparer.OrdinalIgnoreCase)
        {
            ["todo"] = EStatusTask.Todo,
            ["in-progress"] = EStatusTask.InProgress,
            ["progress"] = EStatusTask.InProgress,
            ["done"] = EStatusTask.Done
        };
        public static void HandleCommands(string[] args)
        {
            EStatusTask? filter = null;
            if (args.Length == 0)
            {
                PrintUsage();
            }

            switch (args[0])
            {
                case "add" when args.Length == 2:
                    _th.Add(args[1]);
                    Console.WriteLine($"Task {args[1]} added!");
                    break;

                case "update" when args.Length == 3 && int.TryParse(args[1], out var id):
                    _th.Rename(id, args[2]);
                    Console.WriteLine($"Task {id} edited!");
                    break;

                case "delete" when args.Length == 2 && int.TryParse(args[1], out var delId):
                    _th.Delete(delId);
                    Console.WriteLine($"Task {args[1]} deleted!");
                    break;

                case "mark-progress" when args.Length == 2 && int.TryParse(args[1], out var pId):
                    _th.MarkProgress(pId);
                    Console.WriteLine($"Task {pId} marked as in progress!");
                    break;

                case "mark-done" when args.Length == 2 && int.TryParse(args[1], out var dId):
                    _th.MarkDone(dId);
                    Console.WriteLine($"Task {dId} marked as done!");
                    break;

                case "list" when args.Length <= 2:
                    if (args.Length == 2)
                    {
                        if (!_filters.TryGetValue(args[1], out var status))
                        {
                            PrintUsage();
                            Environment.Exit(0);
                        }
                        filter = status;
                    }

                    var list = _th.List(filter);
                    foreach (var task in list)
                        Console.WriteLine($"ID: {task.Id} |\nDescription: {task.Description} |\nCreated at: {task.CreatedAt.ToString("HH:mm:ss - d/M/yyyy", CultureInfo.InvariantCulture)} |\nUpdated at: {task.UpdatedAt.ToString("HH:mm:ss - d/M/yyyy", CultureInfo.InvariantCulture)} |\nTask status: {task.Status} |\n");
                    break;

                default:
                    PrintUsage();
                    break;

            }


        }
        static void PrintUsage() => Console.Error.WriteLine("""
    Usage:
      tasks add <nome>
      tasks update <id> <nome>
      tasks delete <id>
      tasks mark-progress <id>
      tasks mark-done <id>
      tasks list [todo|in-progress|done]
    """);
    }
}
