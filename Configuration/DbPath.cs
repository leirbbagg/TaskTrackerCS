namespace TaskTrackerCS.Configuration
{
    public static class DbPath
    {
        private static string Folder()
        {
            var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "task-tracker");
            Directory.CreateDirectory(folder);
            return folder;
        }
        public static string Json()
        {
            return Path.Combine(Folder(), "tasks.json");
        }
        public static string Sql()
        {
            return Path.Combine(Folder(), "tasks.db");
        }
    }
}