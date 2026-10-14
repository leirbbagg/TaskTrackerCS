using System.Text.Json;
using System.Text.Json.Serialization;
using TaskTrackerCS.Models;


namespace TaskTrackerCS.Repo
{
    public class TaskRepository
    {
        private readonly string _path;
        private static readonly JsonSerializerOptions _options = new()
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        };

        public TaskRepository(string path = "tasks.json")
        {
            _path = path;
        }

        public List<TaskToDo> Load()
        {
            if (!File.Exists(_path))
                return new List<TaskToDo>();

            var json = File.ReadAllText(_path);
            return JsonSerializer.Deserialize<List<TaskToDo>>(json, _options) ?? new();
        }

        public void Save(List<TaskToDo> tarefas)
        {
            var json = JsonSerializer.Serialize(tarefas, _options);
            File.WriteAllText(_path, json);
        }
    }
}