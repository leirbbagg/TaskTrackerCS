using System;

namespace TaskTrackerCS
{
    public static class CommandHandler
    {
        public static void HandleCommands(string[] args)
        {
            if (args.Length == 0)
            {
                PrintUsage();
            }

            switch (args[0])
            {
                case "add" when args.Length == 2:
                    Console.WriteLine($"add {args[1]}");

                    // * not implemented
                    break;

                case "update" when args.Length == 3 && int.TryParse(args[1], out var id):
                    Console.WriteLine($"update {id} {args[2]}");

                    // * not implemented
                    break;

                case "delete" when args.Length == 2 && int.TryParse(args[1], out var delId):
                    Console.WriteLine($"delete {delId}");

                    // * not implemented
                    break;

                case "mark-progress" when args.Length == 2 && int.TryParse(args[1], out var pId):
                    Console.WriteLine($"mark-in-progress {pId}");

                    // * not implemented
                    break;

                case "mark-done" when args.Length == 2 && int.TryParse(args[1], out var dId):
                    Console.WriteLine($"mark-done {dId}");

                    // * not implemented
                    break;

                case "list" when args.Length <= 2:
                    if (args.Length == 2)
                    {
                        Console.WriteLine($"list {args[1]}");
                    }
                    else
                    {
                        Console.WriteLine($"list");
                    }

                    // * not implemented
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
